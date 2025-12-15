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
    public partial class RegistroFundoImportService(
        IRegistroFundoRepository repository,
        ICadfiRepository cadfiRepository,
        IFalhaRepository falhaRepository,
        ILayoutRepository layoutRepository,
        IConfiguration configuration,
        ILogger<RegistroFundoImportService> logger)
        : BaseImportService<IRegistroFundoRepository, RegistroFundoVO>(repository, cadfiRepository, falhaRepository, layoutRepository, configuration, logger),
        IRegistroFundoImportService
    {
        private readonly ILogger<RegistroFundoImportService> logger = logger;

        public override async Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity)
        {
            var traceId = cadfiEntity.Hash;
            try
            {
                logger.LogInformation("Iniciando importação de registro fundo. TraceId: {TraceId}", traceId);

                string uri = configuration["CVM:CadfiRegistroFundoClasse:uri"]!;
                string file = configuration["CVM:CadfiRegistroFundoClasse:file"]!;
                string fileCsv = configuration["CVM:CadfiRegistroFundoClasse:fileFundo"]!;
                string subfolder = "Adaptados175";

                if (await GetArquivoCvm(uri, file) == false)
                {
                    cadfiEntity.StatusNaoAdaptados175 = StatusEnum.DownloadErro;
                    await UpdateCadfi(cadfiEntity, StatusEnum.DownloadErro);
                    logger.LogError("Erro ao efetuar o download do arquivo de registro fundo. TraceId: {TraceId}", traceId);
                    throw new Exception("Erro efetuar o download do arquivo de registro fundo.");
                }

                await DescompatarArquivo(file, subfolder);

                string curDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString())!;
                string destino = Path.Combine(curDir, "downloads", "cadfi", subfolder, fileCsv);

                var layout = await GetLayout(destino, FormatoCadfiEnum.Fundo);
                if (layout == null)
                {
                    cadfiEntity.StatusNaoAdaptados175 = StatusEnum.LayoutIdentificacaoErro;
                    await UpdateCadfi(cadfiEntity, StatusEnum.LayoutIdentificacaoErro);
                    logger.LogError("Layout não encontrado para o arquivo de registro fundo. TraceId: {TraceId}", traceId);
                    throw new Exception("Layout não encontrado para o arquivo de registro fundo.");
                }

                var table = await BuildDataTable(destino, layout);

                if (table == null || table.Rows.Count == 0)
                {
                    cadfiEntity.StatusNaoAdaptados175 = StatusEnum.ArquivoSemDados;
                    await UpdateCadfi(cadfiEntity, StatusEnum.ArquivoSemDados);
                    logger.LogWarning("Arquivo de registro fundo sem dados. TraceId: {TraceId}", traceId);
                    return false;
                }

                logger.LogInformation("Arquivo de registro fundo lido com {RowCount} linhas. TraceId: {TraceId}", table.Rows.Count, traceId);

                _ = await GetConteudo(cadfiEntity, table, layout);

                logger.LogInformation("Importação de registro fundo finalizada. TraceId: {TraceId}", traceId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro inesperado na importação de registro fundo. TraceId: {TraceId}", cadfiEntity.Hash);
                throw;
            }
        }

        private async Task<bool> GetConteudo(CadfiVO cadfi, DataTable table, LayoutVO layout)
        {
            var traceId = cadfi.Hash;
            try
            {
                var totalLinhas = table.Rows.Count;
                logger.LogInformation("Processando registro fundo. Total de linhas: {TotalLinhas}. TraceId: {TraceId}", totalLinhas, traceId);

                var (details, linhasComErro) = await DataImportProcessor.ProcessDataImportAsync<RegistroFundoVO, CadfiVO>(
                    table,
                    layout,
                    (processed, total) =>
                    {
                        var percentual = processed * 100 / total;
                        logger.LogInformation("Captura CADFI registro fundo: {Percentual}% de linhas processadas. TraceId: {TraceId}", percentual, traceId);
                    },
                    row => new RegistroFundoVO(),
                    (detail, idFk) =>
                    {
                        detail.CadfiId = idFk;
                    },
                    cadfi.Id
                );

                logger.LogInformation("Iniciando gravação de registro fundo: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                await GravarLinhas([.. details], traceId);
                await GravarErros([.. linhasComErro], cadfi.Id);

                logger.LogInformation("Finalizado gravação de registro fundo: {Count} linhas. TraceId: {TraceId}", details.Count, traceId);

                cadfi.StatusRegistroFundo = StatusEnum.ImportacaoOk;
                cadfi.LinhasRegistroFundo = table.Rows.Count;
                cadfi.LinhasImportadasRegistroFundo = details.Count;
                cadfi.LinhasIgnoradasRegistroFundo = table.Rows.Count - details.Count;
                cadfi.LinhasFalhasRegistroFundo = linhasComErro.Count;
                cadfi.LinhasComErrosRegistroFundo = [.. linhasComErro.Select(e => e.Linha)];
                var response = await UpdateCadfi(cadfi, StatusEnum.Processamento);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar registro fundo. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        private async Task GravarLinhas(List<RegistroFundoVO> lista, string traceId)
        {
            try
            {
                await repository.BulkInsertAsync([.. lista]);
                logger.LogInformation("Gravação de registro fundo realizada com sucesso. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao gravar registro fundo em lote. Quantidade: {Count}. TraceId: {TraceId}", lista.Count, traceId);
                throw;
            }
        }
    }
}