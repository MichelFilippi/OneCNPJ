using OneCNPJ.Common.Validacoes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OneCNPJ.Common.Utilities
{
    public static partial class OneExtensoes
    {
        public static string GetDescription(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field!.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name ?? string.Empty;
                }
            }
            return enumValue.ToString(); // Retorna o nome do enum se o Display não estiver definido
        }

        public static bool IsCpf(this string str)
        {
            if (str.Length == 11) { return ValidaCPF.IsValidCpf(str); }
            else if (str.Length == 14) { return ValidaCPF.IsValidCpf(NonDigitRegex().Replace(str, "")); }
            else { return false; }
        }

        public static bool IsCnpj(this string str)
        {
            if (str.Length == 14) { return ValidaCNPJ.IsValidCnpj(str); }
            else if (str.Length == 18) { return ValidaCNPJ.IsValidCnpj(NonDigitRegex().Replace(str, "")); }
            else { return false; }
        }

        public static bool IsCpfCnpj(this string str)
        {
            if (NonDigitRegex().Replace(str, "").Length == 11) { return ValidaCPF.IsValidCpf(str); }
            else if (NonDigitRegex().Replace(str, "").Length == 14) { return ValidaCNPJ.IsValidCnpj(str); }
            else { return false; }
        }

        [GeneratedRegex(@"[^\d]")]
        private static partial Regex NonDigitRegex();
    }
}
