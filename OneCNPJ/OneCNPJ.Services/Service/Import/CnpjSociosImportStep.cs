using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.Data;
using OneCNPJ.Domain;
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
    public class CnpjSociosImportStep(
          IDbContextFactory<ApplicationDbContext> contextFactory,
          IHttpClientFactory httpClientFactory,
          ICnpjImportacaoRepository importacaoRepository,
          ILogger<CnpjSociosImportStep> logger
      ) : ICnpjSociosImportStep
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ILogger<CnpjSociosImportStep> _logger = logger;

        public async Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId)
        {
            importacao.StatusSocios = StatusEnum.Processamento;
            await _importacaoRepository.Update(importacao);

            var http = _httpClientFactory.CreateClient();
            var workDir = Path.Combine(Path.GetTempPath(), "onecnpj", $"import_{importacao.Id}", "socios");
            Directory.CreateDirectory(workDir);

            try
            {
                _logger.LogInformation("TraceId {TraceId} - Listando ZIPs RFB (Sócios)...", traceId);
                var allZips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);

                var socioZips = RfbOpenDataHelper.FilterByName(allZips, "SOCIO", "SOCIOS").ToList();
                if (!socioZips.Any())
                    throw new Exception("Nenhum ZIP de Sócios encontrado no diretório público.");

                long total = 0;
                long importadas = 0;
                long falhas = 0;

                foreach (var zipUrl in socioZips)
                {
                    var zipPath = await RfbOpenDataHelper.DownloadZipAsync(http, zipUrl, workDir, traceId);
                    var extractFolder = Path.Combine(workDir, "extract_" + Path.GetFileNameWithoutExtension(zipPath));
                    var csvPath = RfbOpenDataHelper.ExtractZipToFolder(zipPath, extractFolder);

                    _logger.LogInformation("TraceId {TraceId} - Processando Sócios CSV: {Csv}", traceId, csvPath);

                    // Layout esperado (RFB) comum:
                    // 0 basico,1 tipo_socio,2 nome_socio,3 doc_socio,4 qualif_socio,5 data_entrada
                    var rows = RfbOpenDataHelper.ReadCsvRows(csvPath).Select(fields =>
                    {
                        total++;
                        try
                        {
                            var basico = (fields.ElementAtOrDefault(0) ?? "0").Trim();
                            var tipo = (fields.ElementAtOrDefault(1) ?? "").Trim();
                            var nome = (fields.ElementAtOrDefault(2) ?? "").Trim();
                            var doc = (fields.ElementAtOrDefault(3) ?? "").Trim();
                            var qualif = (fields.ElementAtOrDefault(4) ?? "0").Trim();

                            return new CnpjSocio
                            {
                                ImportacaoId = importacao.Id,
                                CnpjBasico = long.TryParse(basico, out var b) ? b : 0,
                                TipoSocio = tipo,
                                NomeSocio = nome,
                                DocumentoSocio = doc,
                                QualificacaoSocioId = long.TryParse(qualif, out var q) ? q : 0,
                                DataEntradaSociedade = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(5)),
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
                    foreach (var batch in RfbOpenDataHelper.Batch(rows, 5000))
                    {
                        await ctx.CnpjSocios.AddRangeAsync(batch);
                        await ctx.SaveChangesAsync();
                        importadas += batch.Count;
                    }
                }

                importacao.LinhasSociosTotal = total;
                importacao.LinhasSociosImportadas = importadas;
                importacao.LinhasSociosFalhas = falhas;
                importacao.StatusSocios = falhas > 0 ? StatusEnum.ImportacaoErro : StatusEnum.ImportacaoOk;
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return importacao.StatusSocios == StatusEnum.ImportacaoOk;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraceId {TraceId} - Erro ao importar Sócios", traceId);

                importacao.StatusSocios = StatusEnum.ImportacaoErro;
                importacao.Mensagem = $"Erro Sócios: {ex.Message}";
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return false;
            }
        }
    }
}
