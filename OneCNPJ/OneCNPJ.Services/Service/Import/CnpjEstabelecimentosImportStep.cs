using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Service.Import
{
    public class CnpjEstabelecimentosImportStep(
           IDbContextFactory<ApplicationDbContext> contextFactory,
           IHttpClientFactory httpClientFactory,
           ICnpjImportacaoRepository importacaoRepository,
           ILogger<CnpjEstabelecimentosImportStep> logger
       ) : ICnpjEstabelecimentosImportStep
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ILogger<CnpjEstabelecimentosImportStep> _logger = logger;

        public async Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId)
        {
            importacao.StatusEstabelecimentos = StatusEnum.Processamento;
            await _importacaoRepository.Update(importacao);

            var http = _httpClientFactory.CreateClient();
            var workDir = Path.Combine(Path.GetTempPath(), "onecnpj", $"import_{importacao.Id}", "estabelecimentos");
            Directory.CreateDirectory(workDir);

            try
            {
                using var ctxLookup = _contextFactory.CreateDbContext();

                var mapEmpresa = await ctxLookup.CnpjEmpresas
                    .AsNoTracking()
                    .Where(e => e.ImportacaoId == importacao.Id)
                    .Select(e => new { e.Id, e.CnpjBasico })
                    .ToDictionaryAsync(x => x.CnpjBasico, x => x.Id);

                _logger.LogInformation("TraceId {TraceId} - Listando ZIPs RFB (Estabelecimentos)...", traceId);
                var allZips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);

                // Nomes costumam conter "ESTAB"
                var estabZips = RfbOpenDataHelper.FilterByName(allZips, "ESTAB", "ESTABELE").ToList();
                if (!estabZips.Any())
                    throw new Exception("Nenhum ZIP de Estabelecimentos encontrado no diretório público.");

                long total = 0;
                long importadas = 0;
                long falhas = 0;

                foreach (var zipUrl in estabZips)
                {
                    var zipPath = await RfbOpenDataHelper.DownloadZipAsync(http, zipUrl, workDir, traceId);
                    var extractFolder = Path.Combine(workDir, "extract_" + Path.GetFileNameWithoutExtension(zipPath));
                    var csvPath = RfbOpenDataHelper.ExtractZipToFolder(zipPath, extractFolder);

                    _logger.LogInformation("TraceId {TraceId} - Processando Estabelecimentos CSV: {Csv}", traceId, csvPath);

                    // Layout esperado (RFB) — campos principais; adapte se necessário:
                    // 0 basico,1 ordem,2 dv,3 matriz/filial,4 nome_fantasia,5 situacao,6 data_sit,7 motivo,
                    // 8 cidade_exterior,9 pais,10 inicio_atividade,11 cnae_principal,12 cnae_secundaria,
                    // 13 tipo_logr,14 logr,15 numero,16 compl,17 bairro,18 cep,19 uf,20 municipio
                    var rows = RfbOpenDataHelper.ReadCsvRows(csvPath).Select(fields =>
                    {
                        total++;
                        try
                        {
                            var basico = (fields.ElementAtOrDefault(0) ?? "").Trim();
                            if (!mapEmpresa.TryGetValue(basico, out var empresaId))
                            {
                                falhas++;
                                return null!;
                            }

                            var ordem = (fields.ElementAtOrDefault(1) ?? "").Trim();
                            var dv = (fields.ElementAtOrDefault(2) ?? "").Trim();

                            var motivo = (fields.ElementAtOrDefault(7) ?? "0").Trim();
                            var pais = (fields.ElementAtOrDefault(9) ?? "").Trim();
                            var cnae = (fields.ElementAtOrDefault(11) ?? "0").Trim();
                            var municipio = (fields.ElementAtOrDefault(20) ?? "0").Trim();

                            return new CnpjEstabelecimento
                            {
                                ImportacaoId = importacao.Id,
                                CnpjBasico = long.TryParse(basico, out var b) ? b : 0,
                                CnpjEmpresaId = empresaId,
                                CnpjOrdem = ordem,
                                CnpjDv = dv,
                                IdentificadorMatrizFilial = (fields.ElementAtOrDefault(3) ?? "").Trim(),
                                NomeFantasia = (fields.ElementAtOrDefault(4) ?? "").Trim(),
                                SituacaoCadastral = (fields.ElementAtOrDefault(5) ?? "").Trim(),
                                DataSituacaoCadastral = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(6)),
                                MotivoSituacaoCadastralId = long.TryParse(motivo, out var ms) ? ms : 0,
                                NomeCidadeExterior = (fields.ElementAtOrDefault(8) ?? "").Trim(),
                                PaisId = long.TryParse(pais, out var pid) ? pid : null,
                                DataInicioAtividade = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(10)),
                                CnaeId = long.TryParse(cnae, out var cnp) ? cnp : 0,
                                TipoLogradouro = (fields.ElementAtOrDefault(13) ?? "").Trim(),
                                Logradouro = (fields.ElementAtOrDefault(14) ?? "").Trim(),
                                Numero = (fields.ElementAtOrDefault(15) ?? "").Trim(),
                                Complemento = (fields.ElementAtOrDefault(16) ?? "").Trim(),
                                Bairro = (fields.ElementAtOrDefault(17) ?? "").Trim(),
                                Cep = (fields.ElementAtOrDefault(18) ?? "").Trim(),
                                Uf = (fields.ElementAtOrDefault(19) ?? "").Trim(),
                                MunicipioId = long.TryParse(municipio, out var mid) ? mid : 0,
                                Status = StatusEnum.ImportacaoOk,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow
                            };
                        }
                        catch
                        {
                            falhas++;
                            return null!;
                        }
                    }).Where(x => x != null).ToList();

                    using var ctx = _contextFactory.CreateDbContext();
                    foreach (var batch in RfbOpenDataHelper.Batch(rows, 3000))
                    {
                        await ctx.CnpjEstabelecimentos.AddRangeAsync(batch);
                        await ctx.SaveChangesAsync();
                        importadas += batch.Count;
                    }

                    // CNAE secundário: normalmente vem como lista em um campo único (ex.: "1234,5678")
                    // Se você já tiver a coluna de secundários separada, aqui você cria registros em cnpj_estab_cnae_sec.
                    // (deixei “starter” para você adaptar ao layout real que estiver vindo)
                }

                importacao.LinhasEstabelecimentosTotal = total;
                importacao.LinhasEstabelecimentosImportadas = importadas;
                importacao.LinhasEstabelecimentosFalhas = falhas;
                importacao.StatusEstabelecimentos = falhas > 0 ? StatusEnum.ImportacaoErro : StatusEnum.ImportacaoOk;
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return importacao.StatusEstabelecimentos == StatusEnum.ImportacaoOk;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraceId {TraceId} - Erro ao importar Estabelecimentos", traceId);

                importacao.StatusEstabelecimentos = StatusEnum.ImportacaoErro;
                importacao.Mensagem = $"Erro Estabelecimentos: {ex.Message}";
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return false;
            }
        }
    }
}