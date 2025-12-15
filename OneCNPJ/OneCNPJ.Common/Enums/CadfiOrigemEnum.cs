using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Common.Enums
{
    public enum CadfiOrigemEnum
    {
        [Description("Não Adaptados RCVM175")]
        [Display(Name = "Não Adaptados RCVM175")]
        NaoAdaptados175 = 1,

        [Description("Fundos de Investimento, Classes e Subclasses de Cotas")]
        [Display(Name = "Fundos de Investimento, Classes e Subclasses de Cotas")]
        FundosClassesSubclasses = 2
    }
}
