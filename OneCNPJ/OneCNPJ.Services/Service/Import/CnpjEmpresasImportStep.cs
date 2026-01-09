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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Service.Import
{
    public class CnpjEmpresasImportStep(
          IDbContextFactory<ApplicationDbContext> contextFactory,
          IHttpClientFactory httpClientFactory,
          ICnpjImportacaoRepository importacaoRepository,
          ILogger<CnpjEmpresasImportStep> logger
      ) : ICnpjEmpresasImportStep
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ILogger<CnpjEmpresasImportStep> _logger = logger;

        public async Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId)
        {
            importacao.StatusEmpresas = StatusEnum.Processamento;
            await _importacaoRepository.Update(importacao);

            var http = _httpClientFactory.CreateClient();
            var workDir = Path.Combine(Path.GetTempPath(), "onecnpj", $"import_{importacao.Id}", "empresas");
            Directory.CreateDirectory(workDir);

            try
            {
                _logger.LogInformation("TraceId {TraceId} - Listando ZIPs RFB (Empresas)...", traceId);
                var allZips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);

                // Nomes no site costumam conter "EMPRE" (empresas)
                var empresaZips = RfbOpenDataHelper.FilterByName(allZips, "EMPRE", "EMPRESAS").ToList();
                if (!empresaZips.Any())
                    throw new Exception("Nenhum ZIP de Empresas encontrado no diretório público.");

                long total = 0;
                long importadas = 0;
                long falhas = 0;

                // Processa TODOS os ZIPs do dataset
                foreach (var zipUrl in empresaZips)
                {
                    var zipPath = await RfbOpenDataHelper.DownloadZipAsync(http, zipUrl, workDir, traceId);
                    var extractFolder = Path.Combine(workDir, "extract_" + Path.GetFileNameWithoutExtension(zipPath));
                    var csvPath = RfbOpenDataHelper.ExtractZipToFolder(zipPath, extractFolder);

                    _logger.LogInformation("TraceId {TraceId} - Processando Empresas CSV: {Csv}", traceId, csvPath);

                    // Layout esperado (RFB): 0 basico, 1 razao, 2 natureza, 3 qualif resp, 4 capital, 5 porte, 6 ente
                    IEnumerable<CnpjEmpresa> rows = RfbOpenDataHelper.ReadCsvRows(csvPath)
                        .Select(fields =>
                        {
                            total++;
                            try
                            {
                                var basico = (fields.ElementAtOrDefault(0) ?? "").Trim();
                                var razao = (fields.ElementAtOrDefault(1) ?? "").Trim();
                                var natureza = (fields.ElementAtOrDefault(2) ?? "0").Trim();
                                var qualifResp = (fields.ElementAtOrDefault(3) ?? "0").Trim();
                                var capital = (fields.ElementAtOrDefault(4) ?? "").Trim();
                                var porte = (fields.ElementAtOrDefault(5) ?? "0").Trim();
                                var ente = (fields.ElementAtOrDefault(6) ?? "").Trim();

                                return new CnpjEmpresa
                                {
                                    ImportacaoId = importacao.Id,
                                    CnpjBasico = basico,
                                    RazaoSocial = razao,
                                    NaturezaJuridicaId = long.TryParse(natureza, out var nj) ? nj : 0,
                                    QualificacaoSocioId = long.TryParse(qualifResp, out var qr) ? qr : 0,
                                    CapitalSocial = capital,
                                    Porte = Enum.TryParse<PorteEmpresaEnum>(porte, out var pe) ? pe : PorteEmpresaEnum.NaoInformado,
                                    EnteFederativoResponsavel = string.IsNullOrWhiteSpace(ente) ? null : ente,
                                    Status = StatusEnum.ImportacaoOk,
                                    DataCriacao = DateTime.UtcNow,
                                    DataAtualizacao = DateTime.UtcNow
                                };
                            }
                            catch
                            {
                                falhas++;
                                // retorna null e filtra depois
                                return null!;
                            }
                        })
                        .Where(x => x != null);

                    using var ctx = _contextFactory.CreateDbContext();

                    // Inserção em lote (EF) — depois você troca por COPY (Npgsql) se quiser
                    foreach (var batch in RfbOpenDataHelper.Batch(rows, 5000))
                    {
                        await ctx.CnpjEmpresas.AddRangeAsync(batch);
                        await ctx.SaveChangesAsync();
                        importadas += batch.Count;
                    }
                }

                importacao.LinhasEmpresasTotal = total;
                importacao.LinhasEmpresasImportadas = importadas;
                importacao.LinhasEmpresasFalhas = falhas;
                importacao.StatusEmpresas = falhas > 0 ? StatusEnum.ImportacaoErro : StatusEnum.ImportacaoOk;
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);

                return importacao.StatusEmpresas == StatusEnum.ImportacaoOk;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraceId {TraceId} - Erro ao importar Empresas", traceId);

                importacao.StatusEmpresas = StatusEnum.ImportacaoErro;
                importacao.Mensagem = $"Erro Empresas: {ex.Message}";
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return false;
            }
        }
    }
}