using Microsoft.EntityFrameworkCore;
using OneCNPJ.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("registro_classe")]
    public class RegistroClasse 
        : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [ForeignKey("registro_fundo_id")]
        public long? RegistroFundoId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual RegistroFundo? RegistroFundo { get; set; }

        [Column("cabecalho")]
        public List<string> Cabecalho { get; set; } = [];

        [Column("id_registro_fundo")]
        public long IdRegistroFundo { get; set; }

        [Column("id_registro_classe")]
        public long IdRegistroClasse { get; set; }

        [Column("cnpj_classe")]
        public string CnpjClasse { get; set; } = string.Empty;

        [Column("codigo_cvm")]
        [MaxLength(7)]
        public string? CodigoCvm { get; set; }

        [Column("data_registro")]
        public DateOnly? DataRegistro { get; set; }

        [Column("data_constituicao")]
        public DateOnly? DataConstituicao { get; set; }

        [Column("data_inicio")]
        public DateOnly? DataInicio { get; set; }

        [Column("tipo_classe")]
        [MaxLength(100)]
        public string? TipoClasse { get; set; } = string.Empty;

        [Column("denominacao_social")]
        [MaxLength(100)]
        public string DenominacaoSocial { get; set; } = string.Empty;

        [Column("situacao")]
        [MaxLength(100)]
        public string? Situacao { get; set; } = string.Empty;

        [Column("classificacao")]
        [MaxLength(100)]
        public string? Classificacao { get; set; } = string.Empty;

        [Column("data_inicio_situacao")]
        public DateOnly? DataInicioSituacao { get; set; }

        [Column("indicador_desempenho")]
        [MaxLength(100)]
        public string? IndicadorDesempenho { get; set; } = string.Empty;

        [Column("classe_cotas")]
        [MaxLength(1)]
        public string? ClasseCotas { get; set; }

        [Column("classe_anbima")]
        [MaxLength(100)]
        public string? ClasseAnbima { get; set; } = string.Empty;

        [Column("tributacao_longo_prazo")]
        [MaxLength(3)]
        public string? TributacaoLongoPrazo { get; set; } = string.Empty;

        [Column("entidade_investimento")]
        [MaxLength(1)]
        public string? EntidadeInvestimento { get; set; }

        [Column("permitido_aplicacao_cem_por_cento_exterior")]
        [MaxLength(1)]
        public string? PermitidoAplicacaoCemPorCentoExterior { get; set; }

        [Column("classe_esg")]
        [MaxLength(1)]
        public string? ClasseEsg { get; set; }

        [Column("forma_condominio")]
        [MaxLength(100)]
        public string? FormaCondominio { get; set; } = string.Empty;

        [Column("exclusivo")]
        [MaxLength(1)]
        public string? Exclusivo { get; set; }

        [Column("publico_alvo")]
        [MaxLength(15)]
        public string? PublicoAlvo { get; set; } = string.Empty;

        [Column("patrimonio_liquido", TypeName = "decimal(28,2)")]
        public decimal? PatrimonioLiquido { get; set; }

        [Column("data_patrimonio_liquido")]
        public DateOnly? DataPatrimonioLiquido { get; set; }

        [Column("cnpj_auditor")]
        [MaxLength(18)]
        public string? CnpjAuditor { get; set; } = string.Empty;

        [Column("auditor")]
        [MaxLength(100)]
        public string? Auditor { get; set; } = string.Empty;

        [Column("cnpj_custodiante")]
        [MaxLength(18)]
        public string? CnpjCustodiante { get; set; } = string.Empty;

        [Column("custodiante")]
        [MaxLength(100)]
        public string? Custodiante { get; set; } = string.Empty;

        [Column("cnpj_controlador")]
        [MaxLength(18)]
        public string? CnpjControlador { get; set; } = string.Empty;

        [Column("controlador")]
        [MaxLength(100)]
        public string? Controlador { get; set; } = string.Empty;

        public RegistroClasse() { }
    }
}
