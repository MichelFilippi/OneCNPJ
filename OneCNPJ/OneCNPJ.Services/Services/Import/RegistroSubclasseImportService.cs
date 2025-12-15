using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites;
using OneCNPJ.Services.Interfaces.Import;
using OneCNPJ.Services.Utilities;
using System.Data;

namespace OneCNPJ.Services.Services.Import
{
    public class RegistroSubclasseImportService(
        IRegistroSubclasseRepository repository,
        ICadfiRepository cadfiRepository,
        IFalhaRepository falhaRepository,
        ILayoutRepository layoutRepository,
        IRegistroClasseRepository classeRepository,
        IConfiguration configuration,
        ILogger<RegistroSubclasseImportService> logger)
        : BaseImportService<IRegistroSubclasseRepository, RegistroSubclasseVO>(repository, cadfiRepository, falhaRepository, layoutRepository, configuration, logger),
        IRegistroSubclasseImportService
    {
        private readonly IRegistroClasseRepository classeRepository = classeRepository;
        private readonly ILogger<RegistroSubclasseImportService> logger = logger;

        public override async Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity)
        {
            var traceId = cadfiEntity.Hash;
            try
            {
                logger.LogInformation("Iniciando importação de registro subclasse. TraceId: {TraceId}", traceId);

                string fileCsv = configuration["CVM:CadfiRegistroFundoClasse:fileSubclasse"]!;
                string subfolder = "Adaptados175";
                string curDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString())!;
                string destino = Path.Combine(curDir, "downloads", "cadfi", subfolder, fileCsv);

                var layout = await GetLayout(destino, FormatoCadfiEnum.Subclasse);
                if (layout == null)
                {
                    cadfiEntity.StatusRegistroSubclasse = StatusEnum.LayoutIdentificacaoErro;
                    await UpdateCadfi(cadfiEntity, StatusEnum.LayoutIdentificacaoErro);
                    logger.LogError("Layout não encontrado para o arquivo de registro subclasse. TraceId: {TraceId}", traceId);
                    throw new Exception("Layout não encontrado para o arquivo de registro subclasse.");
                }

                var table = await BuildDataTable(destino, layout);

                if (table == null || table.Rows.Count == 0)
                {
                    cadfiEntity.StatusRegistroSubclasse = StatusEnum.ArquivoSemDados;
                    await UpdateCadfi(cadfiEntity, StatusEnum.ArquivoSemDados);
                    logger.LogWarning("Arquivo de registro subclasse sem dados. TraceId: {TraceId}", traceId);
                    return false;
                }

                logger.LogInformation("Arquivo de registro subclasse lido com {RowCount} linhas. TraceId: {TraceId}", table.Rows.Count, traceId);

                _ = await GetConteudo(cadfiEntity, table, layout);

                logger.LogInformation("Importação de registro subclasse finalizada. TraceId: {TraceId}", traceId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro inesperado na importação de registro subclasse. TraceId: {TraceId}", cadfiEntity.Hash);
                throw;
            }
        }

        private async Task<bool> GetConteudo(CadfiVO cadfi, DataTable table, LayoutVO layout)
        {
            var traceId = cadfi.Hash;
            try
            {
                var totalLinhas = table.Rows.Count;
                logger.LogInformation("Processando registro subclasse. Total de linhas: {TotalLinhas}. TraceId: {TraceId}", totalLinhas, traceId);

                var (details, linhasComErro) = await DataImportProcessor.ProcessDataImportAsync<RegistroSubclasseVO, CadfiVO>(
                    table,
                    layout,
                    (processed, total) =>
                    {
                        var percentual = processed * 100 / total;
                        logger.LogInformation("Captura CADFI registro subclasse: {Percentual}% de linhas processadas. TraceId: {TraceId}", percentual, traceId);
                    },
                    row => new RegistroSubclasseVO(),
                    (detail, idFk) =>
                    {
                        detail.CadfiId = idFk;
                    },
                    cadfi.Id
                );

                logger.LogInformation("Iniciando gravação de registro subclasse: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                await GravarLinhas([.. details], cadfi.Id, traceId);
                await GravarErros([.. linhasComErro], cadfi.Id);

                logger.LogInformation("Finalizado gravação de registro subclasse: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                cadfi.StatusRegistroSubclasse = StatusEnum.ImportacaoOk;
                cadfi.LinhasRegistroSubclasse = table.Rows.Count;
                cadfi.LinhasImportadasRegistroSubclasse = details.Count;
                cadfi.LinhasIgnoradasRegistroSubclasse = table.Rows.Count - details.Count;
                cadfi.LinhasFalhasRegistroSubclasse = linhasComErro.Count;
                cadfi.LinhasComErrosRegistroSubclasse = [.. linhasComErro.Select(e => e.Linha)];
                var response = await UpdateCadfi(cadfi, StatusEnum.Processamento);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar registro subclasse. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        private async Task GravarLinhas(List<RegistroSubclasseVO> lista, long cadfiId, string traceId)
        {
            try
            {
                var classeIdDict = await GetClasseIdDictionaryAsync(cadfiId);

                foreach (var item in lista)
                {
                    if (classeIdDict.TryGetValue(item.IdRegistroClasse, out var registroClasseId))
                    {
                        item.CadfiId = cadfiId;
                        item.RegistroClasseId = registroClasseId;
                    }
                }

                await repository.BulkInsertAsync([.. lista]);
                logger.LogInformation("Gravação de registro subclasse realizada com sucesso. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao gravar registro subclasse em lote. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
                throw;
            }
        }

        private async Task<Dictionary<long, long>> GetClasseIdDictionaryAsync(long cadfiId)
        {
            var classes = await classeRepository.GetTodosPorCadfiIdAsync(cadfiId);

            Dictionary<long, long> dict;
            try
            {
                dict = classes
                    .Where(f => f != null)
                    .GroupBy(f => f.IdRegistroClasse)
                    .Select(g => g.First())
                    .ToDictionary(f => f.IdRegistroClasse, f => f.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar dicionário de classes para registro subclasse. CadfiId: {CadfiId}", cadfiId);
                throw;
            }

            return dict;
        }
    }
}