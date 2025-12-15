using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Common.Enums
{
    public enum OrigemCadastroEnum
    {
        [Description("Não definido")]
        [Display(Name = "Não definido")]
        Indefinido = 0,

        [Description("API SEFAZ")]
        [Display(Name = "API SEFAZ")]
        Sefaz = 1,

        [Description("APP CRIA")]
        [Display(Name = "APP CRIA")]
        AppCria = 2,

        [Description("Importação")]
        [Display(Name = "Importação")]
        Importacao = 3,

        [Description("Integração via API")]
        [Display(Name = "Integração via API")]
        Integracao = 4,

        [Description("Integração interna")]
        [Display(Name = "Integração interna")]
        Interno = 5,

        [Description("Integração CVM (RFC)")]
        [Display(Name = "Integração CVM (RFC)")]
        CvmRfc = 6,

        [Description("Integração CVM (Conteúdo)")]
        [Display(Name = "Integração CVM (Conteúdo)")]
        CvmConteudo = 7
    }
}
