using ExcelDataReader;
using System.IO.Compression;
using System.Text;

namespace OneCNPJ.Common.Utilities
{
    public class Files
    {
        public static async Task ExtractZipFile(string sourceZipFilePath, string destinationDirectory)
        {
            await Task.Run(() =>
            {
                // Verifica se a pasta de destino já existe
                if (Directory.Exists(destinationDirectory))
                {
                    // Exclui a pasta de destino e todo o seu conteúdo
                    Directory.Delete(destinationDirectory, true);
                }

                // Cria a pasta de destino novamente
                Directory.CreateDirectory(destinationDirectory);

                // Extrai o arquivo ZIP para a pasta de destino
                if (File.Exists(sourceZipFilePath))
                {
                    ZipFile.ExtractToDirectory(sourceZipFilePath, destinationDirectory);
                }
                else
                {
                    Console.WriteLine("Arquivo ZIP não encontrado.");
                }
            });
        }

        public static int GetFileLineCount(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (extension == ".txt" || extension == ".csv")
            {
                // TXT/CSV: conta as linhas
                return File.ReadLines(filePath, Encoding.GetEncoding(1252)).Count();
            }
            else if (extension == ".xls" || extension == ".xlsx")
            {
                // XLS/XLSX: conta as linhas da primeira planilha
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();
                var table = result.Tables[0];
                return table.Rows.Count;
            }
            else
            {
                // Outros formatos não suportados
                throw new NotSupportedException("Formato de arquivo não suportado para contagem de linhas.");
            }
        }

        public static async Task<(List<string> Headers, int TotalLines)> GetFileHeadersAndLineCount(string filePath, int cabecalho = 0)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            int totalLines = 0;
            List<string> headers = [];

            if (extension == ".csv")
            {
                // Conta linhas de forma eficiente
                totalLines = File.ReadLines(filePath, Encoding.GetEncoding(1252)).Count();

                // Lê a linha do cabeçalho de forma assíncrona
                using var reader = new StreamReader(filePath, Encoding.GetEncoding(1252));
                string? headerLine = null;
                for (int i = 0; i <= cabecalho; i++)
                {
                    headerLine = await reader.ReadLineAsync();
                    if (headerLine == null) return ([], totalLines);
                }

                if (headerLine == null) return ([], totalLines);
                var separator = headerLine.Contains(';') ? ';' : ',';
                headers = [.. headerLine.Split(separator).Select(h => h.Trim())];
            }
            else if (extension == ".xls" || extension == ".xlsx")
            {
                // XLS/XLSX: conta as linhas da primeira planilha
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var excelReader = ExcelReaderFactory.CreateReader(stream);
                var result = excelReader.AsDataSet();
                var table = result.Tables[0];
                totalLines = table.Rows.Count;

                if (table.Rows.Count > cabecalho)
                {
                    var headerRow = table.Rows[cabecalho];
                    headers = [];
                    foreach (var item in headerRow.ItemArray)
                    {
                        headers.Add(item?.ToString()?.Trim() ?? "");
                    }
                }
            }
            else
            {
                throw new NotSupportedException("Formato de arquivo não suportado para leitura de cabeçalhos.");
            }

            return (headers, totalLines);
        }
    }
}
