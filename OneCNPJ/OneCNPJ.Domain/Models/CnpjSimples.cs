
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace OneCNPJ.Domain.Models
{
    [Table("cnpj_simples")]
    public class CnpjSimples : BaseModel, IEntity
    {
        [Column("cnpj_basico")]
        public long CnpjBasico { get; set; }

        [Column("cnpj_empresa")]
        public long CnpjEmpresaId { get; set; }

        [ForeignKey(nameof(CnpjEmpresaId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? CnpjEmpresa { get; set; }

        [ForeignKey(nameof(CnpjBasico))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? Empresa { get; set; }

        [Column("optante_simples")]
        public bool OptanteSimples { get; set; }

        [Column("data_opcao_simples")]
        public DateTime? DataOpcaoSimples { get; set; }

        [Column("data_exclusao_simples")]
        public DateTime? DataExclusaoSimples { get; set; }

        [Column("optante_mei")]
        public bool OptanteMei { get; set; }

        [Column("data_opcao_mei")]
        public DateTime? DataOpcaoMei { get; set; }

        [Column("data_exclusao_mei")]
        public DateTime? DataExclusaoMei { get; set; }

        public CnpjSimples() { }
    }
}