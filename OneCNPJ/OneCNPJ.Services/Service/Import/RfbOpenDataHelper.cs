using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Service.Import
{
    public static class RfbOpenDataHelper
    {
        // Diretório público
        public const string BaseUrl = "https://arquivos.receitafederal.gov.br/dados/cnpj/dados_abertos_cnpj/";

        private static readonly Regex ZipHrefRegex =
            new Regex("href=\"([^\"]+\\.zip)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex DirHrefRegex =
         new Regex("href=\"([^\"]+/)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex VersionDirRegex =
            new Regex(@"^\d{4}-\d{2}/$", RegexOptions.Compiled);

        public static string OnlyDigits(string s)
            => new string((s ?? "").Where(char.IsDigit).ToArray());

        public static DateTime? ParseDateYYYYMMDD(string? raw)
        {
            raw = (raw ?? "").Trim();
            if (string.IsNullOrWhiteSpace(raw) || raw == "0") return null;

            // alguns arquivos usam YYYYMMDD
            if (raw.Length == 8 &&
                int.TryParse(raw.Substring(0, 4), out var y) &&
                int.TryParse(raw.Substring(4, 2), out var m) &&
                int.TryParse(raw.Substring(6, 2), out var d))
            {
                try { return new DateTime(y, m, d); } catch { return null; }
            }

            // fallback: tenta parse normal
            if (DateTime.TryParse(raw, out var dt)) return dt;
            return null;
        }

        public static async Task<List<string>> ListZipLinksAsync(HttpClient http, string traceId)
        {
            // 1) carrega o HTML da raiz
            var rootHtml = await http.GetStringAsync(BaseUrl);

            // 2) descobre pastas de versão (YYYY-MM/)
            var dirMatches = DirHrefRegex.Matches(rootHtml);

            var versionDirs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Match m in dirMatches)
            {
                var href = m.Groups[1].Value.Trim();

                // ignora parent/temp e qualquer coisa que não seja versão
                if (href.Equals("../", StringComparison.OrdinalIgnoreCase)) continue;
                if (href.StartsWith("temp", StringComparison.OrdinalIgnoreCase)) continue;

                if (VersionDirRegex.IsMatch(href))
                    versionDirs.Add(href);
            }

            if (versionDirs.Count == 0)
                throw new InvalidOperationException($"Nenhuma pasta de versão (YYYY-MM/) encontrada em {BaseUrl}");

            // 3) pega a mais recente por nome (YYYY-MM ordena bem)
            var latestDir = versionDirs
                .OrderByDescending(x => x, StringComparer.OrdinalIgnoreCase)
                .First();

            var versionBaseUrl = BaseUrl.TrimEnd('/') + "/" + latestDir.TrimStart('/');

            // 4) carrega o HTML da pasta de versão e lista ZIPs
            var versionHtml = await http.GetStringAsync(versionBaseUrl);

            var zipMatches = ZipHrefRegex.Matches(versionHtml);

            var list = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Match m in zipMatches)
            {
                var href = m.Groups[1].Value.Trim();
                if (string.IsNullOrWhiteSpace(href)) continue;

                if (href.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    list.Add(href);
                else
                    list.Add(versionBaseUrl.TrimEnd('/') + "/" + href.TrimStart('/'));
            }

            if (list.Count == 0)
                throw new InvalidOperationException($"Nenhum ZIP encontrado em {versionBaseUrl}");

            return list
                .OrderByDescending(x => x, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }


        public static IEnumerable<string> FilterByName(IEnumerable<string> zips, params string[] containsAny)
        {
            return zips.Where(z =>
                containsAny.Any(k => z.Contains(k, StringComparison.OrdinalIgnoreCase)));
        }

        public static async Task<string> DownloadZipAsync(HttpClient http, string zipUrl, string folder, string traceId)
        {
            Directory.CreateDirectory(folder);

            var fileName = Path.GetFileName(new Uri(zipUrl).AbsolutePath);
            var filePath = Path.Combine(folder, fileName);

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                return filePath;

            using var stream = await http.GetStreamAsync(zipUrl);
            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await stream.CopyToAsync(fs);

            return filePath;
        }

        public static string ExtractZipToFolder(string zipPath, string extractFolder)
        {
            if (!File.Exists(zipPath))
                throw new FileNotFoundException($"ZIP não encontrado: {zipPath}");

            Directory.CreateDirectory(extractFolder);

            // limpa pasta para evitar sobras de execuções anteriores
            foreach (var f in Directory.GetFiles(extractFolder, "*", System.IO.SearchOption.AllDirectories))
                File.Delete(f);

            ZipFile.ExtractToDirectory(zipPath, extractFolder, overwriteFiles: true);

            var files = Directory.GetFiles(extractFolder, "*.*", System.IO.SearchOption.AllDirectories);

            // 1) tenta CSV/TXT
            var dataFile = files
                .Where(f =>
                {
                    var ext = Path.GetExtension(f);
                    return ext.Equals(".csv", StringComparison.OrdinalIgnoreCase)
                        || ext.Equals(".txt", StringComparison.OrdinalIgnoreCase);
                })
                .OrderByDescending(f => new FileInfo(f).Length)
                .FirstOrDefault();

            // 2) fallback: qualquer arquivo não-zip
            dataFile ??= files
                .Where(f => !Path.GetExtension(f).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(f => new FileInfo(f).Length)
                .FirstOrDefault();

            if (dataFile == null)
                throw new FileNotFoundException($"Nenhum arquivo de dados encontrado dentro do ZIP: {zipPath}");
            Console.WriteLine($"Arquivos extraídos de {zipPath}:");
            foreach (var f in files)
                Console.WriteLine(" - " + Path.GetFileName(f));

            return dataFile;
        }

        // Leitura robusta de CSV com ; e aspas
        public static IEnumerable<string[]> ReadCsvRows(string csvPath, bool hasHeader = false)
        {
            using var parser = new TextFieldParser(csvPath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            parser.HasFieldsEnclosedInQuotes = true;
            parser.TrimWhiteSpace = true;

            if (hasHeader && !parser.EndOfData)
                _ = parser.ReadFields();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields == null) continue;
                yield return fields;
            }
        }

        public static IEnumerable<List<T>> Batch<T>(IEnumerable<T> source, int size)
        {
            var batch = new List<T>(size);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count >= size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }
            if (batch.Count > 0) yield return batch;
        }
    }
}