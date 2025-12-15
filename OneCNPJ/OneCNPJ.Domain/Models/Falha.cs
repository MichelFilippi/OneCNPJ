using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("falha")]
    public class Falha : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [Column("linha")]
        public int Linha { get; set; }

        [Column("linha_conteudo")]
        public List<string> LinhaConteudo { get; set; } = [];

        [Column("coluna_origem")]
        public List<string> ColunaOrigem { get; set; } = [];

        [Column("campo_destino")]
        public List<string> CampoDestino { get; set; } = [];

        [Column("valor_recebido")]
        public List<string> ValorRecebido { get; set; } = [];

        [Column("motivo")]
        public List<string> Motivo { get; set; } = [];

        public Falha() { }
    }
}
