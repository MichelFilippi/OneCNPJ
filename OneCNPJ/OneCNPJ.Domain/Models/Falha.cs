using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Domain.Models
{
    [Table("falha")]
    public class Falha : BaseModel, IEntity
    {
        [ForeignKey("cnpj_empresa_id")]
        public long CnpjEmpresaId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? CnpjEmpresa { get; set; }

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
