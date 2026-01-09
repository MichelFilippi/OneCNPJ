


using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Domain.Models {
    [Table("cnpj_empresa")]
    public class CnpjEmpresa : BaseModel, IEntity
    {
        [Column("importacao_id")]
        public long ImportacaoId { get; set; }

        [ForeignKey(nameof(ImportacaoId))]
        public virtual CnpjImportacao? Importacao { get; set; }

        [Column("cnpj_basico")]
        public string CnpjBasico { get; set; } = string.Empty; 

        [Column("razao_social")]
        public string RazaoSocial { get; set; } = string.Empty;

        [Column("natureza_juridica_id")]
        public long NaturezaJuridicaId { get; set; } 

        [ForeignKey(nameof(NaturezaJuridicaId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual NaturezaJuridica? NaturezaJuridica { get; set; }

        [Column("qualificacao_socio_id")]
        public long QualificacaoSocioId { get; set; } 

        [ForeignKey(nameof(QualificacaoSocioId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual QualificacaoSocio? QualificacaoSocio { get; set; }

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