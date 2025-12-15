using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Common.Enums
{
    public enum PermissoesEnum
    {
        [Description("Blacklist (todos exceto quem está na lista)")]
        [Display(Name = "Blacklist (todos exceto quem está na lista)")]
        BlackList = -1,

        [Description("Todos")]
        [Display(Name = "Todos")]
        Todos = 0,

        [Description("Whitelist (somente quem está na lista)")]
        [Display(Name = "Whitelist (somente quem está na lista)")]
        WhiteList = 1
    }
}
