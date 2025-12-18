using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Domain.Models
{
    [Table("ignorado")]
    public class Ignorado : BaseModel, IEntity
    {
        [ForeignKey("cnpj_empresa_id")]
        public long CnpjEmpresaId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? CnpjEmpresa { get; set; }

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
