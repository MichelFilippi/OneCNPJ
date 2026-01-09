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
    public class CnpjSimplesImportStep(
          IDbContextFactory<ApplicationDbContext> contextFactory,
          IHttpClientFactory httpClientFactory,
          ICnpjImportacaoRepository importacaoRepository,
          ILogger<CnpjSimplesImportStep> logger
      ) : ICnpjSimplesImportStep
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ILogger<CnpjSimplesImportStep> _logger = logger;

        private static bool ParseBoolSimNao(string? raw)
        {
            raw = (raw ?? "").Trim().ToUpperInvariant();
            // RFB geralmente usa "S"/"N"
            return raw == "S" || raw == "1" || raw == "TRUE";
        }

        public async Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId)
        {
            importacao.StatusSimples = StatusEnum.Processamento;
            await _importacaoRepository.Update(importacao);

            var http = _httpClientFactory.CreateClient();
            var workDir = Path.Combine(Path.GetTempPath(), "onecnpj", $"import_{importacao.Id}", "simples");
            Directory.CreateDirectory(workDir);

            try
            {
                using var ctxLookup = _contextFactory.CreateDbContext();
                var mapEmpresa = await ctxLookup.CnpjEmpresas
                    .AsNoTracking()
                    .Where(e => e.ImportacaoId == importacao.Id)
                    .Select(e => new { e.Id, e.CnpjBasico })
                    .ToDictionaryAsync(x => x.CnpjBasico, x => x.Id);

                _logger.LogInformation("TraceId {TraceId} - Listando ZIPs RFB (Simples)...", traceId);
                var allZips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);

                var simplesZips = RfbOpenDataHelper.FilterByName(allZips, "SIMPLES").ToList();
                if (!simplesZips.Any())
                    throw new Exception("Nenhum ZIP de Simples encontrado no diretório público.");

                long total = 0;
                long importadas = 0;
                long falhas = 0;

                foreach (var zipUrl in simplesZips)
                {
                    var zipPath = await RfbOpenDataHelper.DownloadZipAsync(http, zipUrl, workDir, traceId);
                    var extractFolder = Path.Combine(workDir, "extract_" + Path.GetFileNameWithoutExtension(zipPath));
                    var csvPath = RfbOpenDataHelper.ExtractZipToFolder(zipPath, extractFolder);

                    _logger.LogInformation("TraceId {TraceId} - Processando Simples CSV: {Csv}", traceId, csvPath);

                    // Layout esperado (comum):
                    // 0 basico,1 opt_simples,2 dt_opcao_simples,3 dt_exclusao_simples,4 opt_mei,5 dt_opcao_mei,6 dt_exclusao_mei
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

                            return new CnpjSimples
                            {
                                ImportacaoId = importacao.Id,
                                CnpjBasico = long.TryParse(basico, out var b) ? b : 0,
                                CnpjEmpresaId = empresaId,

                                OptanteSimples = ParseBoolSimNao(fields.ElementAtOrDefault(1)),
                                DataOpcaoSimples = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(2)),
                                DataExclusaoSimples = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(3)),

                                OptanteMei = ParseBoolSimNao(fields.ElementAtOrDefault(4)),
                                DataOpcaoMei = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(5)),
                                DataExclusaoMei = RfbOpenDataHelper.ParseDateYYYYMMDD(fields.ElementAtOrDefault(6)),

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
                        await ctx.CnpjSimples.AddRangeAsync(batch);
                        await ctx.SaveChangesAsync();
                        importadas += batch.Count;
                    }
                }

                importacao.LinhasSimplesTotal = total;
                importacao.LinhasSimplesImportadas = importadas;
                importacao.LinhasSimplesFalhas = falhas;
                importacao.StatusSimples = falhas > 0 ? StatusEnum.ImportacaoErro : StatusEnum.ImportacaoOk;
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return importacao.StatusSimples == StatusEnum.ImportacaoOk;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraceId {TraceId} - Erro ao importar Simples", traceId);

                importacao.StatusSimples = StatusEnum.ImportacaoErro;
                importacao.Mensagem = $"Erro Simples: {ex.Message}";
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return false;
            }
        }
    }
}