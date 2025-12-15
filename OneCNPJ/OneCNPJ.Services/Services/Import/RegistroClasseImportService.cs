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
    public class RegistroClasseImportService(
        IRegistroClasseRepository repository,
        ICadfiRepository cadfiRepository,
        IFalhaRepository falhaRepository,
        ILayoutRepository layoutRepository,
        IRegistroFundoRepository fundoRepository,
        IConfiguration configuration,
        ILogger<RegistroClasseImportService> logger)
        : BaseImportService<IRegistroClasseRepository, RegistroClasseVO>(repository, cadfiRepository, falhaRepository, layoutRepository, configuration, logger),
        IRegistroClasseImportService
    {
        private readonly IRegistroFundoRepository fundoRepository = fundoRepository;
        private readonly ILogger<RegistroClasseImportService> logger = logger;

        public override async Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity)
        {
            var traceId = cadfiEntity.Hash;
            try
            {
                logger.LogInformation("Iniciando importação de registro classe. TraceId: {TraceId}", traceId);

                string fileCsv = configuration["CVM:CadfiRegistroFundoClasse:fileClasse"]!;
                string subfolder = "Adaptados175";
                string curDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString())!;
                string destino = Path.Combine(curDir, "downloads", "cadfi", subfolder, fileCsv);

                var layout = await GetLayout(destino, FormatoCadfiEnum.Classe);
                if (layout == null)
                {
                    cadfiEntity.StatusRegistroClasse = StatusEnum.LayoutIdentificacaoErro;
                    await UpdateCadfi(cadfiEntity, StatusEnum.LayoutIdentificacaoErro);
                    logger.LogError("Layout não encontrado para o arquivo de registro classe. TraceId: {TraceId}", traceId);
                    throw new Exception("Layout não encontrado para o arquivo de registro classe.");
                }

                var table = await BuildDataTable(destino, layout);

                if (table == null || table.Rows.Count == 0)
                {
                    cadfiEntity.StatusRegistroClasse = StatusEnum.ArquivoSemDados;
                    await UpdateCadfi(cadfiEntity, StatusEnum.ArquivoSemDados);
                    logger.LogWarning("Arquivo de registro classe sem dados. TraceId: {TraceId}", traceId);
                    return false;
                }

                logger.LogInformation("Arquivo de registro classe lido com {RowCount} linhas. TraceId: {TraceId}", table.Rows.Count, traceId);

                _ = await GetConteudo(cadfiEntity, table, layout);

                logger.LogInformation("Importação de registro classe finalizada. TraceId: {TraceId}", traceId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro inesperado na importação de registro classe. TraceId: {TraceId}", cadfiEntity.Hash);
                throw;
            }
        }

        private async Task<bool> GetConteudo(CadfiVO cadfi, DataTable table, LayoutVO layout)
        {
            var traceId = cadfi.Hash;
            try
            {
                var totalLinhas = table.Rows.Count;
                logger.LogInformation("Processando registro classe. Total de linhas: {TotalLinhas}. TraceId: {TraceId}", totalLinhas, traceId);

                var (details, linhasComErro) = await DataImportProcessor.ProcessDataImportAsync<RegistroClasseVO, CadfiVO>(
                    table,
                    layout,
                    (processed, total) =>
                    {
                        var percentual = processed * 100 / total;
                        logger.LogInformation("Captura CADFI registro classe: {Percentual}% de linhas processadas. TraceId: {TraceId}", percentual, traceId);
                    },
                    row => new RegistroClasseVO(),
                    (detail, idFk) =>
                    {
                        detail.CadfiId = idFk;
                    },
                    cadfi.Id
                );

                logger.LogInformation("Iniciando gravação de registro classe: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                await GravarLinhas([.. details], cadfi.Id, traceId);
                await GravarErros([.. linhasComErro], cadfi.Id);

                logger.LogInformation("Finalizado gravação de registro classe: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                cadfi.StatusRegistroClasse = StatusEnum.ImportacaoOk;
                cadfi.LinhasRegistroClasse = table.Rows.Count;
                cadfi.LinhasImportadasRegistroClasse = details.Count;
                cadfi.LinhasIgnoradasRegistroClasse = table.Rows.Count - details.Count;
                cadfi.LinhasFalhasRegistroClasse = linhasComErro.Count;
                cadfi.LinhasComErrosRegistroClasse = [.. linhasComErro.Select(e => e.Linha)];
                var response = await UpdateCadfi(cadfi, StatusEnum.Processamento);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar registro classe. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        private async Task GravarLinhas(List<RegistroClasseVO> lista, long cadfiId, string traceId)
        {
            try
            {
                var fundoIdDict = await GetFundoIdDictionaryAsync(cadfiId);

                foreach (var item in lista)
                {
                    if (fundoIdDict.TryGetValue(item.IdRegistroFundo, out var registroFundoId))
                    {
                        item.CadfiId = cadfiId;
                        item.RegistroFundoId = registroFundoId;
                    }
                }

                await repository.BulkInsertAsync([.. lista]);
                logger.LogInformation("Gravação de registro classe realizada com sucesso. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao gravar registro classe em lote. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
                throw;
            }
        }

        private async Task<Dictionary<long, long>> GetFundoIdDictionaryAsync(long cadfiId)
        {
            var fundos = await fundoRepository.GetTodosPorCadfiIdAsync(cadfiId);

            Dictionary<long, long> dict;
            try
            {
                dict = fundos
                    .Where(f => f != null)
                    .GroupBy(f => f.IdRegistroFundo)
                    .Select(g => g.First())
                    .ToDictionary(f => f.IdRegistroFundo, f => f.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar dicionário de fundos para registro classe. CadfiId: {CadfiId}", cadfiId);
                throw;
            }

            return dict;
        }
    }
}