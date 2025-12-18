using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain
{


    [Table("cnpj_socio")]
    public class CnpjSocio : BaseModel, IEntity
    {
        [Column("cnpj_basico")]
        public long CnpjBasico { get; set; } 

        [ForeignKey(nameof(CnpjBasico))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? Empresa { get; set; }

        [Column("tipo_socio")]
        public string TipoSocio { get; set; } = string.Empty; // PF/PJ (ou código do layout)

        [Column("nome_socio")]
        public string NomeSocio { get; set; } = string.Empty;

        [Column("documento_socio")]
        public string DocumentoSocio { get; set; } = string.Empty;

        [Column("qualificacao_socio_id")]
        public long QualificacaoSocioId { get; set; } 

        [ForeignKey(nameof(QualificacaoSocioId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual QualificacaoSocio? QualificacaoSocio { get; set; }

        [Column("data_entrada_sociedade")]
        public DateTime? DataEntradaSociedade { get; set; }

        public CnpjSocio() { }
    }
}
