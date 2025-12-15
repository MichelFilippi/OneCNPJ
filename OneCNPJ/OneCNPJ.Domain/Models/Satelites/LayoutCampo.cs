using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models.Satelites
{
    [Table("layout_campo")]
    public class LayoutCampo : BaseModel, IEntity
    {
        [Column("cabecalho")]
        [MaxLength(50)]
        [Required]
        public string Cabecalho { get; set; } = string.Empty;

        [Column("cabecalho_ordem")]
        [MaxLength(5)]
        [Required]
        public string CabecalhoOrdem { get; set; } = string.Empty;

        [Column("obrigatorio")]
        public bool Obrigatorio { get; set; } = false;

        [Column("atributo_classe")]
        [MaxLength(100)]
        public string AtributoClasse { get; set; } = string.Empty;

        [Column("atributo_classe_amigavel")]
        [MaxLength(200)]
        public string AtributoClasseAmigavel { get; set; } = string.Empty;

        [Column("model_obrigatorio")]
        public bool ModelObrigatorio { get; set; } = false;

        [Column("ordem")]
        public int Ordem { get; set; } = int.MaxValue;

        public long LayoutId { get; set; }
        [ForeignKey("LayoutId")]
        public Layout Layout { get; set; } = null!;
    }
}
