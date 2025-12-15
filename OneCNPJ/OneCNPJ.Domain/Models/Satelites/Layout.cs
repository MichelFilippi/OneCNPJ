using OneCNPJ.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models.Satelites
{
    [Table("layout")]
    public class Layout : BaseModel, IEntity
    {
        [Column("descricao")]
        [MaxLength(100)]
        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Column("linha_cabecalho")]
        [Required]
        public int LinhaCabecalho { get; set; }

        [Column("linha_dados")]
        [Required]
        public int LinhaDados { get; set; }

        [Column("formato_cadfi")]
        public FormatoCadfiEnum FormatoCadfiEnum { get; set; } = FormatoCadfiEnum.NaoDefinido;

        public List<LayoutCampo> Campos { get; set; } = [];

        public Layout() { }
    }
}
