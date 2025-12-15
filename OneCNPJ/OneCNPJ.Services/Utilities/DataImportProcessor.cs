using OneCNPJ.Common.Utilities;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Satelites;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace OneCNPJ.Services.Utilities
{
    public partial class DataImportProcessor
    {
        public static Task<(ConcurrentBag<TDetail> Details, ConcurrentBag<FalhaVO> LinhasComErro)> ProcessDataImportAsync<TDetail, TEntity>(
            DataTable table,
            LayoutVO layout,
            Action<int, int> reportProgress,
            Func<DataRow, TDetail> createDetailFunc,
            Action<TDetail, long> finalizeDetailFunc,
            long idFk)
            where TDetail : new()
        {
            var columnsLayout = layout.Campos;
            var details = new ConcurrentBag<TDetail>();
            var linhasComErro = new ConcurrentBag<FalhaVO>();
            var totalLinhas = table.Rows.Count;
            var milestone = Math.Max(1, totalLinhas / 10);
            var contador = 0;

            Console.WriteLine($"Iniciando captura: {totalLinhas} linhas [{DateTime.Now:dd/MM/yyyy HH:mm:ss}]");

            int maxHeaderColumn = table.Columns.Count;
            LayoutCampoVO fieldLinha = new()
            {
                AtributoClasse = "Linha",
                Cabecalho = "Linha",
                CabecalhoOrdem = General.ExcelIntParaLetraColuna(maxHeaderColumn)
            };
            columnsLayout.Add(fieldLinha);

            // Crie uma lista de tuplas para permitir colunas duplicadas
            var columnMapping = columnsLayout.Select(field => (field.CabecalhoOrdem, field.AtributoClasse!)).ToList();
            var properties = typeof(TDetail).GetProperties().ToDictionary(p => p.Name);

            bool endProcess = false;

            Parallel.For(0, table.Rows.Count, (i, state) =>
            {
                var stopwatch = Stopwatch.StartNew();
                if (endProcess)
                {
                    Console.WriteLine($"Parando processo [{DateTime.Now:dd/MM/yyyy HH:mm:ss}]");
                    state.Stop();
                    return;
                }

                var row = table.Rows[i];
                var fileLine = i + layout.LinhaDados;
                var detail = createDetailFunc(row);
                var originalLine = string.Join(";", row.ItemArray);
                bool nonImport = false;

                foreach (var field in columnsLayout)
                {
                    try
                    {
                        if (!ProcessarRegras(table.Columns.Count, row, field, properties, detail))
                        {
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        AdicionarFalha(linhasComErro, row, fileLine, field, row[General.ExcelLetraColunaParaIntBase0(field.CabecalhoOrdem)].ToString()!);
                    }
                }

                if (nonImport) { return; }

                finalizeDetailFunc(detail, idFk);
                details.Add(detail);

                Interlocked.Increment(ref contador);
                if (contador % milestone == 0)
                {
                    var percentual = contador * 100 / totalLinhas;
                    reportProgress(contador, totalLinhas);
                }

                stopwatch.Stop();
            });

            Console.WriteLine($"Finalizado captura: {totalLinhas} linhas [{DateTime.Now:dd/MM/yyyy HH:mm:ss}]");

            return Task.FromResult((details, linhasComErro));
        }

        private static bool ProcessarRegras<TDetail>(int columns, DataRow row, LayoutCampoVO field, Dictionary<string, PropertyInfo> properties, TDetail detail)
            where TDetail : new()
        {
            int columnIndex = General.ExcelLetraColunaParaIntBase0(field.CabecalhoOrdem);
            if (columnIndex >= 0 && columnIndex < columns)
            {
                var value = row[columnIndex];
                // Verificar se a coluna é nula

                if (properties.TryGetValue(field.AtributoClasse!, out var property) && value != DBNull.Value)
                {
                    var valueString = value.ToString();

                    if (ConvertionAndSetValue(property, valueString!, detail))
                    {
                        // Truncar valores que excedem o comprimento máximo, exceto para atributos marcados com [DocumentoMF]
                        var maxLengthAttribute = property.GetCustomAttribute<MaxLengthAttribute>();

                        if (maxLengthAttribute != null && valueString != null && valueString.Length > maxLengthAttribute.Length)
                        {
                            valueString = valueString[..maxLengthAttribute.Length];
                            ConvertionAndSetValue(property, valueString, detail);
                        }
                    }
                }
            }
            return true;
        }

        private static bool ConvertionAndSetValue<TDetail>(PropertyInfo property, string valueString, TDetail detail) where TDetail : new()
        {
            object? convertionedValue = null;

            if ((property.PropertyType == typeof(DateOnly) || property.PropertyType == typeof(DateOnly?)) && !string.IsNullOrEmpty(valueString))
            {
                if (DateTime.TryParse(valueString, out var dateTimeValue))
                {
                    convertionedValue = DateOnly.FromDateTime(dateTimeValue);
                }
            }
            else if ((property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?)) && !string.IsNullOrEmpty(valueString))
            {
                // Remove símbolo de moeda e espaços
                valueString = valueString.Replace("R$", "", StringComparison.OrdinalIgnoreCase).Trim();

                // Se contém vírgula, trata como formato brasileiro (milhar com ponto, decimal com vírgula)
                if (valueString.Contains(','))
                {
                    // Remove pontos (milhar)
                    valueString = valueString.Replace(".", "");
                    // Exemplo: "20.540,67" -> "20540,67"
                }
                else if (valueString.Count(c => c == '.') == 1 && !valueString.Any(c => c == ','))
                {
                    // Exemplo: "0.253834" (decimal com ponto, padrão internacional)
                    // Troca ponto por vírgula para pt-BR
                    valueString = valueString.Replace(".", ",");
                }
                // Se não, mantém como está (ex: "20540,67")

                if (decimal.TryParse(valueString, NumberStyles.Any, new CultureInfo("pt-BR"), out var decimalValue))
                {
                    convertionedValue = decimalValue;
                }
            }
            else if ((property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)) && !string.IsNullOrEmpty(valueString))
            {
                // Tentar convertioner o valor usando o formato de moeda brasileira
                if (int.TryParse(valueString, NumberStyles.Any, new CultureInfo("pt-BR"), out var intValue))
                {
                    convertionedValue = intValue;
                }
            }
            else if ((property.PropertyType == typeof(long) || property.PropertyType == typeof(long?)) && !string.IsNullOrEmpty(valueString))
            {
                // Tentar converter o valor para long
                if (long.TryParse(valueString, NumberStyles.Any, new CultureInfo("pt-BR"), out var longValue))
                {
                    convertionedValue = longValue;
                }
            }
            else if ((property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?)) && !string.IsNullOrEmpty(valueString))
            {
                // Tentar converter o valor para bool
                if (bool.TryParse(valueString, out var boolValue))
                {
                    convertionedValue = boolValue;
                }
                else
                {
                    // Tratar valores customizados como "1" ou "0" para true/false
                    valueString = valueString.Trim();
                    if (valueString == "1" || valueString.Equals("true", StringComparison.OrdinalIgnoreCase))
                    {
                        convertionedValue = true;
                    }
                    else if (valueString == "0" || valueString.Equals("false", StringComparison.OrdinalIgnoreCase))
                    {
                        convertionedValue = false;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(valueString))
            {
                convertionedValue = valueString;
            }

            if (convertionedValue != null)
            {
                property.SetValue(detail, convertionedValue);
                return true;
            }

            return false;
        }

        private static void AdicionarFalha(ConcurrentBag<FalhaVO> linhasComErro, DataRow linhaAtual, int linha, LayoutCampoVO field, string valor)
        {
            var falhaDetail = new FalhaVO
            {
                Linha = linha + 1,
                LinhaConteudo = [JsonSerializer.Serialize(linhaAtual.ItemArray)],
                ColunaOrigem = [field.Cabecalho],
                CampoDestino = [field.AtributoClasseAmigavel ?? field.AtributoClasse!],
                ValorRecebido = [valor],
                Motivo = ["Falha na conversão dos dados"]
            };
            linhasComErro.Add(falhaDetail);
        }
    }
}
