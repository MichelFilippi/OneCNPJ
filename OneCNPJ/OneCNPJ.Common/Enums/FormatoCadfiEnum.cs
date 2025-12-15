using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Common.Enums
{
    public enum FormatoCadfiEnum
    {
        [Description("Não definido")]
        [Display(Name = "Não definido")]
        NaoDefinido = 0,

        [Description("Conteudo - Não adaptados 175")]
        [Display(Name = "Conteudo - Não adaptados 175")]
        Conteudo = 1,

        [Description("Fundo - Adaptados 175")]
        [Display(Name = "Fundo - Adaptados 175")]
        Fundo = 2,

        [Description("Classe - Adaptados 175")]
        [Display(Name = "Classe - Adaptados 175")]
        Classe = 3,

        [Description("Subclasse - Adaptados 175")]
        [Display(Name = "Subclasse - Adaptados 175")]
        Subclasse = 4,
    }
}
