using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("ignorado")]
    public class Ignorado : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [Column("linha")]
        public long Linha { get; set; }

        [Column("cabecalho")]
        public List<string> Cabecalho { get; set; } = [];

        [Column("conteudo")]
        public List<string> Conteudo { get; set; } = [];

        [Column("motivo")]
        public List<string> Motivo { get; set; } = [];

        public Ignorado() { }
    }
}
