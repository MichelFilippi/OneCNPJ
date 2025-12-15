using ExcelDataReader;
using System.Text;
using System.Text.RegularExpressions;

namespace OneCNPJ.Common.Utilities
{
    public static partial class General
    {
        public static string GenerateSalt(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxyzZ0123456789!#@$%&";
            var random = new Random();
            var result = new string(
                [.. Enumerable.Repeat(chars, count).Select(s => s[random.Next(s.Length)])]);
            return result;
        }

        public static string CnpjFormatado(string cnpj, bool onlyNumbers)
        {
            if (cnpj == null || !cnpj.IsCnpj()) { return ""; }

            if (onlyNumbers) { return NonDigitRegex().Replace(cnpj, ""); }
            else { return CnpjFormatRegex().Replace(NonDigitRegex().Replace(cnpj, ""), @"$1.$2.$3/$4-$5"); }
        }

        public static string CpfFormatado(this string cpf, bool onlyNumbers = false)
        {
            if (cpf == null || !cpf.IsCpf()) { return ""; }

            if (onlyNumbers) { return NonDigitRegex().Replace(cpf, ""); }
            else { return CpfFormatRegex().Replace(NonDigitRegex().Replace(cpf, ""), @"$1.$2.$3-$4"); }
        }

        public static string DocumentoFormatado(string? documento, bool onlyNumbers = false)
        {
            if (documento == null || (!documento.IsCpf() && !documento.IsCnpj())) { return ""; }

            if (documento.IsCpf()) { return CpfFormatado(documento, onlyNumbers); }
            else { return CnpjFormatado(documento, onlyNumbers); }
        }

        public static int ExcelLetraColunaParaIntBase1(string columnLetter)
        {
            if (string.IsNullOrEmpty(columnLetter)) { return int.MaxValue; }
            if (int.TryParse(columnLetter, out int columnNumber)) { return columnNumber; }

            columnLetter = columnLetter.ToUpperInvariant();
            int column = 0;

            foreach (char c in columnLetter)
            {
                column = column * 26 + c - 'A' + 1;
            }

            return column;
        }

        public static int ExcelLetraColunaParaIntBase0(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) { return int.MaxValue; }
            if (int.TryParse(columnName, out int columnNumber)) { return columnNumber; }

            columnName = columnName.ToUpperInvariant();
            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum - 1;
        }

        public static (int row, int col) ExcelRefToIndex(string excelRef)
        {
            if (string.IsNullOrWhiteSpace(excelRef)) { throw new ArgumentException("Referência Excel inválida.", nameof(excelRef)); }

            var match = LetrasENumeros().Match(excelRef);
            if (!match.Success) { throw new ArgumentException("Referência Excel inválida.", nameof(excelRef)); }

            string colLetters = match.Groups[1].Value.ToUpper();
            int rowNumber = int.Parse(match.Groups[2].Value);

            int colIndex = ExcelLetraColunaParaIntBase0(colLetters);
            int rowIndex = rowNumber - 1;

            return (rowIndex, colIndex);
        }

        public static string ExcelIntParaLetraColuna(int columnNumber)
        {
            if (columnNumber < 1) { throw new ArgumentOutOfRangeException(nameof(columnNumber), "O número da coluna deve ser maior que zero."); }

            StringBuilder columnLetter = new();
            while (columnNumber > 0)
            {
                columnNumber--;
                columnLetter.Insert(0, (char)('A' + (columnNumber % 26)));
                columnNumber /= 26;
            }
            return columnLetter.ToString();
        }

        [GeneratedRegex(@"[^\d]")]
        private static partial Regex NonDigitRegex();

        [GeneratedRegex(@"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})")]
        private static partial Regex CnpjFormatRegex();

        [GeneratedRegex(@"(\d{3})(\d{3})(\d{3})(\d{2})")]
        private static partial Regex CpfFormatRegex();

        [GeneratedRegex(@"^([A-Z]+)(\d+)$", RegexOptions.IgnoreCase, "pt-BR")]
        private static partial Regex LetrasENumeros();
    }
}
