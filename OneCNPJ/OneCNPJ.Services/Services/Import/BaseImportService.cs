using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.Common.Utilities;
using OneCNPJ.DTOs;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace OneCNPJ.Services.Services.Import
{
    public abstract partial class BaseImportService<TRepository, TEntityVO>(
        TRepository repository,
        ICadfiRepository cadfiRepository,
        IFalhaRepository falhaRepository,
        ILayoutRepository layoutRepository,
        IConfiguration configuration,
        ILogger<BaseImportService<TRepository, TEntityVO>> logger)
        where TRepository : class
        where TEntityVO : BaseVO
    {
        protected readonly TRepository repository = repository;
        protected readonly ICadfiRepository cadfiRepository = cadfiRepository;
        protected readonly IFalhaRepository falhaRepository = falhaRepository;
        protected readonly ILayoutRepository layoutRepository = layoutRepository;
        protected readonly IConfiguration configuration = configuration;
        protected readonly ILogger<BaseImportService<TRepository, TEntityVO>> _logger = logger;

        public abstract Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity);

        protected async Task<bool> GetArquivoCvm(string uriConfigKey, string fileConfigKey, string subfolder = "", string? traceId = null)
        {
            string curDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString())!;
            string arquivo = fileConfigKey!;
            var uri = new Uri(uriConfigKey + arquivo);
            string destino = Path.Combine(curDir, "downloads", "cadfi", subfolder ?? "", arquivo);
            FileInfo fi = new(destino);
            if (fi.Exists && fi.IsReadOnly)
            {
                _logger.LogInformation("Arquivo já existe e está como somente leitura. TraceId: {TraceId}", traceId);
                return true;
            }

            _logger.LogInformation("Iniciando download do arquivo: {Arquivo}. Destino: {Destino}. TraceId: {TraceId}", arquivo, destino, traceId);

            try
            {
                using var client = new HttpClient();
                var response = await client.GetByteArrayAsync(uri);
                Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
                File.WriteAllBytes(destino, response);

                if (File.Exists(destino))
                {
                    _logger.LogInformation("Download realizado com sucesso: {Destino}. TraceId: {TraceId}", destino, traceId);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Download falhou: {Destino}. TraceId: {TraceId}", destino, traceId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao baixar arquivo da CVM: {Arquivo}. TraceId: {TraceId}", arquivo, traceId);
                return false;
            }
        }

        protected async Task DescompatarArquivo(string arquivo, string subfolder = "", string? traceId = null)
        {
            string curDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString())!;
            string destino = Path.Combine(curDir, "downloads", "cadfi", subfolder ?? "");
            try
            {
                await Files.ExtractZipFile(Path.Combine(curDir, "downloads", "cadfi", arquivo), destino);
                _logger.LogInformation("Arquivo descompactado: {Arquivo} em {Destino}. TraceId: {TraceId}", arquivo, destino, traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao descompactar arquivo: {Arquivo}. TraceId: {TraceId}", arquivo, traceId);
                throw;
            }
        }

        protected async Task<bool> UpdateCadfi(CadfiVO entity, StatusEnum status, string? traceId = null)
        {
            entity.Status = status;
            try
            {
                var response = await cadfiRepository.Update(entity);
                _logger.LogInformation("Status do CADFI atualizado para {Status}. TraceId: {TraceId}", status, traceId);
                return response != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do CADFI. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        protected async Task<LayoutVO?> GetLayout(string filePath, FormatoCadfiEnum formato, string? traceId = null)
        {
            try
            {
                var layouts = await layoutRepository.GetTodosOperacionaisPorFormatoCadfiAsync(formato);

                if (layouts == null || !layouts.Any())
                {
                    _logger.LogWarning("Nenhum layout operacional encontrado para formato {Formato}. TraceId: {TraceId}", formato, traceId);
                    return null;
                }

                var (headers, totalLines) = await Files.GetFileHeadersAndLineCount(filePath);

                foreach (var layout in layouts)
                {
                    if (layout.LinhaCabecalho == 0 || layout.LinhaCabecalho > totalLines) { continue; }

                    var uniqueFields = new HashSet<LayoutCampoVO>(layout.Campos, new LayoutCampoVO.ComparadorCabecalhoECampo()).ToList();
                    var layoutFields = uniqueFields.OrderBy(field => General.ExcelLetraColunaParaIntBase1(field.CabecalhoOrdem)).ToList();

                    if (layoutFields.Count != headers.Count) { continue; }

                    int fileHeaderIndex = 0;
                    int fieldsConfirmed = 0;
                    foreach (var field in layoutFields)
                    {
                        while (fileHeaderIndex < headers.Count && !headers[fileHeaderIndex].Equals(field.Cabecalho, StringComparison.OrdinalIgnoreCase))
                        {
                            fileHeaderIndex++;
                        }

                        if (fileHeaderIndex + 1 > headers.Count) { break; }

                        if (headers[fileHeaderIndex].Equals(field.Cabecalho, StringComparison.OrdinalIgnoreCase))
                        {
                            fieldsConfirmed++;
                        }

                        fileHeaderIndex++;
                    }

                    if (fieldsConfirmed == layoutFields.Count)
                    {
                        _logger.LogInformation("Layout identificado para arquivo {FilePath}. TraceId: {TraceId}", filePath, traceId);
                        return layout;
                    }
                }
                _logger.LogWarning("Nenhum layout compatível encontrado para arquivo {FilePath}. TraceId: {TraceId}", filePath, traceId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao identificar layout do arquivo {FilePath}. TraceId: {TraceId}", filePath, traceId);
                throw;
            }
        }

        protected async Task<DataTable> BuildDataTable(string filePath, LayoutVO layout, string? traceId = null)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            var table = new DataTable();
            if (extension == ".csv")
            {
                try
                {
                    await RetirarAspas(filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao retirar aspas do arquivo '{FilePath}'. TraceId: {TraceId}", filePath, traceId);
                    throw;
                }

                string[] lines;
                try
                {
                    lines = await File.ReadAllLinesAsync(filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao ler linhas do arquivo '{FilePath}'. TraceId: {TraceId}", filePath, traceId);
                    throw;
                }

                foreach (var field in layout.Campos)
                {
                    table.Columns.Add(field.CabecalhoOrdem);
                }

                foreach (var line in lines.Skip(layout.LinhaDados - 1))
                {
                    try
                    {
                        var values = line.Split(';');
                        table.Rows.Add(values);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao processar linha: \"{Line}\". TraceId: {TraceId}", line, traceId);
                        throw;
                    }
                }
            }

            _logger.LogInformation("DataTable construído para arquivo {FilePath} com {RowCount} linhas. TraceId: {TraceId}", filePath, table.Rows.Count, traceId);
            return table;
        }

        protected async Task GravarErros(List<FalhaVO> lista, long cadfiId, string? traceId = null)
        {
            var listaAgrupada = lista
                .GroupBy(f => f.Linha)
                .Select(g => new FalhaVO
                {
                    CadfiId = cadfiId,
                    Linha = g.Key,
                    LinhaConteudo = [.. g.SelectMany(f => f.LinhaConteudo)],
                    ColunaOrigem = [.. g.SelectMany(f => f.ColunaOrigem)],
                    CampoDestino = [.. g.SelectMany(f => f.CampoDestino)],
                    ValorRecebido = [.. g.SelectMany(f => f.ValorRecebido)],
                    Motivo = [.. g.SelectMany(f => f.Motivo)]
                })
                .ToList();

            try
            {
                await falhaRepository.BulkInsertAsync([.. listaAgrupada]);
                _logger.LogInformation("Falhas gravadas em lote. Quantidade: {Count}. TraceId: {TraceId}", listaAgrupada.Count, traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gravar falhas em lote. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        // Os métodos estáticos abaixo não recebem logger diretamente, mas podem ser adaptados se necessário.

        public static Encoding DetectFileEncoding(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bom = new byte[4];
            int bytesRead = file.Read(bom, 0, 4);

            if (bytesRead >= 3)
            {
                // UTF-8 BOM
                if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                    return Encoding.UTF8;
                // UTF-16 LE BOM
                if (bom[0] == 0xFF && bom[1] == 0xFE)
                    return Encoding.Unicode;
                // UTF-16 BE BOM
                if (bom[0] == 0xFE && bom[1] == 0xFF)
                    return Encoding.BigEndianUnicode;
                // UTF-32 LE BOM
                if (bytesRead == 4 && bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
                    return Encoding.UTF32;
                // UTF-32 BE BOM
                if (bytesRead == 4 && bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == 0xFE && bom[3] == 0xFF)
                    return new UTF32Encoding(bigEndian: true, byteOrderMark: true);
            }

            // Tenta ler como UTF-8, se falhar, usa 1252
            try
            {
                using var reader = new StreamReader(filePath, Encoding.UTF8, true);
                reader.ReadLine();
                return Encoding.UTF8;
            }
            catch
            {
                return Encoding.GetEncoding(1252);
            }
        }

        protected static async Task<bool> RetirarAspas(string filePath)
        {
            try
            {
                string content;
                using (StreamReader reader = new(filePath, Encoding.GetEncoding(1252)))
                {
                    content = reader.ReadToEnd();
                    content = NormalizarConteudo().Replace(content, "");
                }

                using StreamWriter writer = new(filePath);
                writer.Write(content);
                return true;
            }
            catch (Exception)
            {
                // Não é possível logar aqui pois é estático, mas pode ser adaptado para receber um logger se necessário.
                return await RetirarAspasUtf(filePath);
            }
        }

        protected static Task<bool> RetirarAspasUtf(string filePath)
        {
            try
            {
                var encoding = DetectFileEncoding(filePath);
                string content;
                using (StreamReader reader = new(filePath, encoding))
                {
                    content = reader.ReadToEnd();
                    content = NormalizarConteudo().Replace(content, "");
                }

                using StreamWriter writer = new(filePath, false, encoding);
                writer.Write(content);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                // Não é possível logar aqui pois é estático, mas pode ser adaptado para receber um logger se necessário.
            }
            return Task.FromResult(false);
        }

        [GeneratedRegex(@"[\u2018\u2019\u201a\u201b\u0022\u201c\u201d\u201e\u201f\u301d\u301e\u301f]")]
        private static partial Regex NormalizarConteudo();
    }
}