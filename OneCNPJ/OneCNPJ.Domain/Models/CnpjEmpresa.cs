


using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Domain.Models {
    [Table("cnpj_empresa")]
    public class CnpjEmpresa : BaseModel, IEntity
    {
        [Column("cnpj_basico")]
        public string CnpjBasico { get; set; } = string.Empty; // 8 dígitos

        [Column("razao_social")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("natureza_juridica_id")]
        public string NaturezaJuridicaId { get; set; } = string.Empty;

        [ForeignKey(nameof(NaturezaJuridicaId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual NaturezaJuridica? NaturezaJuridica { get; set; }

        [Column("qualificacao_responsavel_id")]
        public string QualificacaoResponsavelId { get; set; } = string.Empty;

        [ForeignKey(nameof(QualificacaoResponsavelId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual QualificacaoSocio? QualificacaoResponsavel { get; set; }

        [Column("capital_social")]
        public string CapitalSocial { get; set; } = string.Empty;

        [Column("porte")]
        public PorteEmpresaEnum Porte { get; set; } = PorteEmpresaEnum.NaoInformado;

        [Column("ente_federativo_responsavel")]
        public string? EnteFederativoResponsavel { get; set; }

        public virtual List<CnpjSocio> Socios { get; set; } = [];
        public virtual CnpjSimples? Simples { get; set; }
        public virtual List<CnpjEstabelecimento> Estabelecimentos { get; set; } = [];

        public CnpjEmpresa() { }
    }
}