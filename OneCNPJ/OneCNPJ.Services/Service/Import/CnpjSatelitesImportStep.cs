using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.Data;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models.Satelites;
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
    public class CnpjSatelitesImportStep(
           IDbContextFactory<ApplicationDbContext> contextFactory,
           IHttpClientFactory httpClientFactory,
           ICnpjImportacaoRepository importacaoRepository,
           ILogger<CnpjSatelitesImportStep> logger
       ) : ICnpjSatelitesImportStep
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ILogger<CnpjSatelitesImportStep> _logger = logger;

        public async Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId)
        {
            importacao.StatusSatelites = StatusEnum.Processamento;
            await _importacaoRepository.Update(importacao);

            var http = _httpClientFactory.CreateClient();
            var workDir = Path.Combine(Path.GetTempPath(), "onecnpj", $"import_{importacao.Id}", "satelites");
            Directory.CreateDirectory(workDir);

            long importadas = 0;
            long falhas = 0;

            try
            {
                var allZips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);

                var satZips = allZips.Where(u =>
                {
                    var name = u.ToUpperInvariant();
                    return name.Contains("PAIS")
                        || name.Contains("MUNIC")
                        || name.Contains("MOTIV")
                        || name.Contains("CNAE")
                        || name.Contains("NATUR")
                        || name.Contains("NATJU")
                        || name.Contains("QUALIF")
                        || name.Contains("QUALS");
                }).ToList();

                _logger.LogInformation("TraceId {TraceId} - Satélites detectados: {Zips}",
                    traceId, string.Join(", ", satZips.Select(Path.GetFileName)));

                if (!satZips.Any())
                {
                    _logger.LogWarning("TraceId {TraceId} - Nenhum ZIP satélite encontrado. Marcando como OK (sem dados).", traceId);
                    importacao.StatusSatelites = StatusEnum.ImportacaoOk;
                    importacao.DataAtualizacao = DateTime.UtcNow;
                    await _importacaoRepository.Update(importacao);
                    return true;
                }

                using var ctx = await _contextFactory.CreateDbContextAsync();

                // Pré-check (1 vez)
                var natjuCount = await ctx.Set<NaturezaJuridica>().CountAsync();
                var qualCount = await ctx.Set<QualificacaoSocio>().CountAsync();
                _logger.LogInformation("TraceId {TraceId} - Pré-check: NaturezaJuridica={Natju}, QualificacaoSocio={Qual}",
                    traceId, natjuCount, qualCount);

                foreach (var zipUrl in satZips)
                {
                    try
                    {
                        var zipPath = await RfbOpenDataHelper.DownloadZipAsync(http, zipUrl, workDir, traceId);
                        var extractFolder = Path.Combine(workDir, "extract_" + Path.GetFileNameWithoutExtension(zipPath));
                        var csvPath = RfbOpenDataHelper.ExtractZipToFolder(zipPath, extractFolder);

                        _logger.LogInformation("TraceId {TraceId} - Satélite CSV: {Csv}", traceId, csvPath);

                        var upperUrl = zipUrl.ToUpperInvariant();
                        var fileName = Path.GetFileName(csvPath).ToUpperInvariant();

                        long inserted = 0;

                        if (upperUrl.Contains("PAIS") || fileName.Contains("PAIS"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new Pais { Id = id, Descricao = desc });

                        else if (upperUrl.Contains("MUNIC") || fileName.Contains("MUNIC"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new Municipio { Id = id, Descricao = desc });

                        else if (upperUrl.Contains("MOTIV") || fileName.Contains("MOTI"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new MotivoSituacaoCadastral { Id = id, Descricao = desc });

                        else if (upperUrl.Contains("CNAE") || fileName.Contains("CNAE"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new Cnae { Id = id, Descricao = desc });

                        else if (upperUrl.Contains("NATUR") || upperUrl.Contains("NATJU") || fileName.Contains("NATJU"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new NaturezaJuridica { Id = id, Descricao = desc });

                        else if (upperUrl.Contains("QUALIF") || upperUrl.Contains("QUALS") || fileName.Contains("QUAL"))
                            inserted = await ImportarCodigoDescricaoAsync(ctx, csvPath, traceId, (id, desc) => new QualificacaoSocio { Id = id, Descricao = desc });

                        else
                            _logger.LogInformation("TraceId {TraceId} - Satélite ignorado (sem parser): {Zip} | {Csv}", traceId, zipUrl, csvPath);

                        importadas += inserted;
                    }
                    catch (Exception exZip)
                    {
                        falhas++;
                        _logger.LogError(exZip, "TraceId {TraceId} - Erro em ZIP satélite {Zip}", traceId, zipUrl);
                    }
                }

                importacao.LinhasSatelitesImportadas = importadas;
                importacao.LinhasSatelitesFalhas = falhas;
                importacao.StatusSatelites = falhas > 0 ? StatusEnum.ImportacaoErro : StatusEnum.ImportacaoOk;
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return importacao.StatusSatelites == StatusEnum.ImportacaoOk;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraceId {TraceId} - Erro ao importar Satélites", traceId);

                importacao.StatusSatelites = StatusEnum.ImportacaoErro;
                importacao.Mensagem = $"Erro Satélites: {ex.Message}";
                importacao.DataAtualizacao = DateTime.UtcNow;

                await _importacaoRepository.Update(importacao);
                return false;
            }
        }
        private async Task ImportarCnaesAsync(ApplicationDbContext ctx, string csvPath, string traceId)
        {
            // Layout típico: CODIGO;DESCRICAO
            var novos = new List<Cnae>();

            foreach (var row in RfbOpenDataHelper.ReadCsvRows(csvPath).Skip(1))
            {
                if (row.Length < 2) continue;

                var id = ToLong(row[0]);
                if (id <= 0) continue;

                novos.Add(new Cnae
                {
                    Id = id,
                    Descricao = ToStr(row[1]),
                });
            }

            if (novos.Count == 0) return;

            var ids = novos.Select(x => x.Id).ToArray();
            var existentes = await ctx.Set<Cnae>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var toInsert = novos.Where(x => !existentes.Contains(x.Id)).ToList();
            if (toInsert.Count > 0)
                await ctx.Set<Cnae>().AddRangeAsync(toInsert);

            await ctx.SaveChangesAsync();

            _logger.LogInformation("TraceId {TraceId} - CNAEs importados: {Count}", traceId, toInsert.Count);
        }

        private async Task ImportarMunicipiosAsync(ApplicationDbContext ctx, string csvPath, string traceId)
        {
            // Layout típico: CODIGO;DESCRICAO
            var novos = new List<Municipio>();

            foreach (var row in RfbOpenDataHelper.ReadCsvRows(csvPath).Skip(1))
            {
                if (row.Length < 2) continue;

                var id = ToLong(row[0]);
                if (id <= 0) continue;

                novos.Add(new Municipio
                {
                    Id = id,
                    Descricao = ToStr(row[1]),
                });
            }

            if (novos.Count == 0) return;

            var ids = novos.Select(x => x.Id).ToArray();
            var existentes = await ctx.Set<Municipio>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var toInsert = novos.Where(x => !existentes.Contains(x.Id)).ToList();
            if (toInsert.Count > 0)
                await ctx.Set<Municipio>().AddRangeAsync(toInsert);

            await ctx.SaveChangesAsync();

            _logger.LogInformation("TraceId {TraceId} - Municípios importados: {Count}", traceId, toInsert.Count);
        }

        private async Task ImportarPaisesAsync(ApplicationDbContext ctx, string csvPath, string traceId)
        {
            // Layout típico: CODIGO;DESCRICAO
            var novos = new List<Pais>();

            foreach (var row in RfbOpenDataHelper.ReadCsvRows(csvPath).Skip(1))
            {
                if (row.Length < 2) continue;

                var id = ToLong(row[0]);
                if (id <= 0) continue;

                novos.Add(new Pais
                {
                    Id = id,
                    Descricao = ToStr(row[1]),
                });
            }

            if (novos.Count == 0) return;

            var ids = novos.Select(x => x.Id).ToArray();
            var existentes = await ctx.Set<Pais>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var toInsert = novos.Where(x => !existentes.Contains(x.Id)).ToList();
            if (toInsert.Count > 0)
                await ctx.Set<Pais>().AddRangeAsync(toInsert);

            await ctx.SaveChangesAsync();

            _logger.LogInformation("TraceId {TraceId} - Países importados: {Count}", traceId, toInsert.Count);
        }
        private static string[] SplitRfb(string line)
        {
            // RFB vem com ; e sem aspas na maioria dos satélites
            return line.Split(';');
        }

        private static long ToLong(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            return long.Parse(s.Trim());
        }

        private static string ToStr(string? s) => (s ?? string.Empty).Trim();

        private async Task<long> ImportarCodigoDescricaoAsync<TEntity>(
         ApplicationDbContext ctx,
         string csvPath,
         string traceId,
         Func<long, string, TEntity> factory)
         where TEntity : class, IEntity
        {
            var novos = new List<TEntity>();

            foreach (var row in RfbOpenDataHelper.ReadCsvRows(csvPath))
            {
                if (row.Length < 2) continue;

                var id = ToLong(row[0]);
                if (id <= 0) continue;

                var desc = ToStr(row[1]);
                if (string.IsNullOrWhiteSpace(desc)) continue;

                novos.Add(factory(id, desc));
            }

            if (novos.Count == 0)
            {
                _logger.LogWarning("TraceId {TraceId} - CSV sem linhas válidas: {Csv}", traceId, csvPath);
                return 0;
            }

            var ids = novos.Select(x => x.Id).Distinct().ToList();
            var existentes = new HashSet<long>();

            foreach (var chunk in ids.Chunk(5000))
            {
                var chunkExistentes = await ctx.Set<TEntity>()
                    .AsNoTracking()
                    .Where(x => chunk.Contains(x.Id))
                    .Select(x => x.Id)
                    .ToListAsync();

                foreach (var e in chunkExistentes) existentes.Add(e);
            }

            var paraInserir = novos.Where(x => !existentes.Contains(x.Id)).ToList();

            foreach (var chunk in paraInserir.Chunk(5000))
            {
                await ctx.Set<TEntity>().AddRangeAsync(chunk);
                await ctx.SaveChangesAsync();
                ctx.ChangeTracker.Clear();
            }

            _logger.LogInformation("TraceId {TraceId} - Importado {Count} novos itens em {Entity} (lidos={Lidos}, existentes={Existentes})",
                traceId, paraInserir.Count, typeof(TEntity).Name, novos.Count, existentes.Count);

            return paraInserir.Count;
        }
    
    }
}