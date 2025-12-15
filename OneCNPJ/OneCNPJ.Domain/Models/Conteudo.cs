using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("conteudo")]
    public class Conteudo : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [Column("cabecalho")]
        public List<string> Cabecalho { get; set; } = [];

        [Column("tp_fundo")]
        [Display(Name = "Tipo de fundo")]
        [MaxLength(20)]
        public string TipoFundo { get; set; } = string.Empty;

        [Column("cnpj_fundo")]
        [Display(Name = "CNPJ do fundo")]
        [MaxLength(18)]
        public string Cnpj { get; set; } = string.Empty;

        [Column("denom_social")]
        [Display(Name = "Denominação Social")]
        [MaxLength(100)]
        public string DenominacaoSocial { get; set; } = string.Empty;

        [Column("dt_reg")]
        [Display(Name = "Data de registro")]
        public DateOnly? DataRegistro { get; set; }

        [Column("const")]
        [Display(Name = "Data de constituição")]
        public DateOnly? DataConstituicao { get; set; }

        [Column("cd_cvm")]
        [Display(Name = "Código CVM")]
        [MaxLength(7)]
        public string? CodigoCvm { get; set; }

        [Column("dt_cancel")]
        [Display(Name = "Data de cancelamento")]
        public DateOnly? DataCancelamento { get; set; }

        [Column("sit")]
        [Display(Name = "Situação")]
        [MaxLength(100)]
        public string? Situacao { get; set; } = string.Empty;

        [Column("dt_ini_sit")]
        [Display(Name = "Data início da situação")]
        public DateOnly? SituacaoDataInicial { get; set; }

        [Column("dt_ini_ativ")]
        [Display(Name = "Data de início de atividade")]
        public DateOnly? AtividadeDataInicial { get; set; }

        [Column("dt_ini_exerc")]
        [Display(Name = "Data início do exercício social")]
        public DateOnly? ExercicioDataInicial { get; set; }

        [Column("dt_fim_exerc")]
        [Display(Name = "Data fim do exercício social")]
        public DateOnly? ExercicioDataFinal { get; set; }

        [Column("classe")]
        [Display(Name = "Classe")]
        [MaxLength(100)]
        public string? Classe { get; set; } = string.Empty;

        [Column("dt_ini_classe")]
        [Display(Name = "Data de início na classe")]
        public DateOnly? ClasseDataInicial { get; set; }

        [Column("rentab_fundo")]
        [Display(Name = "Forma de rentabilidade do fundo (indicador de desempenho)")]
        [MaxLength(100)]
        public string? Rentabilidade { get; set; } = string.Empty;

        [Column("condom")]
        [Display(Name = "Tipo do condomínio")]
        [MaxLength(100)]
        public string? Condominio { get; set; } = string.Empty;

        [Column("fundo_cotas")]
        [Display(Name = "Indica se é fundo de cotas")]
        [MaxLength(1)]
        public string? Cotas { get; set; }

        [Column("fundo_exclusivo")]
        [Display(Name = "Indica se é fundo exclusivo")]
        [MaxLength(1)]
        public string? Exclusivo { get; set; }

        [Column("trib_lprazo")]
        [Display(Name = "Indica se possui tributação de longo prazo")]
        [MaxLength(3)]
        public string? TributacaoLongoPrazo { get; set; } = string.Empty;

        [Column("publico_alvo")]
        [Display(Name = "Público-alvo")]
        [MaxLength(15)]
        public string? PublicoAlvo { get; set; } = string.Empty;

        [Column("entid_invest")]
        [Display(Name = "Indica se o fundo é entidade de investimento")]
        [MaxLength(1)]
        public string? EntidadeInvestimento { get; set; }

        [Column("taxa_perfm")]
        [Display(Name = "Taxa de performance")]
        [Precision(26, 2)]
        public decimal TaxaPerformance { get; set; } = 0;

        [Column("inf_taxa_perfm")]
        [Display(Name = "Informações Adicionais (Taxa de performance)")]
        [MaxLength(400)]
        public string? TaxaPerformanceInfo { get; set; } = string.Empty;

        [Column("taxa_adm")]
        [Display(Name = "Taxa de administração")]
        [Precision(26, 8)]
        public decimal TaxaAdministracao { get; set; } = 0;

        [Column("inf_taxa_adm")]
        [Display(Name = "Informações Adicionais (Taxa de administração)")]
        [MaxLength(400)]
        public string? TaxaAdministracaoInfo { get; set; } = string.Empty;

        [Column("vl_patrim_liq")]
        [Display(Name = "Valor do patrimônio líquido")]
        [Precision(26, 2)]
        public decimal PatrimonioLiquido { get; set; } = 0;

        [Column("dt_patrim_liq")]
        [Display(Name = "Data do patrimônio líquido")]
        public DateOnly? PatrimonioLiquidoData { get; set; }

        [Column("diretor")]
        [Display(Name = "Nome do Diretor Responsável")]
        [MaxLength(100)]
        public string? Diretor { get; set; } = string.Empty;

        [Column("cnpj_admin")]
        [Display(Name = "CNPJ do Administrador")]
        [MaxLength(18)]
        public string? CnpjAdministrador { get; set; } = string.Empty;

        [Column("admin")]
        [Display(Name = "Nome do Administrador")]
        [MaxLength(100)]
        public string? Administrador { get; set; } = string.Empty;

        [Column("pf_pj_gestor")]
        [Display(Name = "Indica se o gestor é pessoa física ou jurídica")]
        [MaxLength(2)]
        public string? TipoGestor { get; set; }

        [Column("cpf_cnpj_gestor")]
        [Display(Name = "Informa o código de identificação do gestor pessoa física ou jurídica")]
        [MaxLength(18)]
        public string? DocumentoGestor { get; set; } = string.Empty;

        [Column("gestor")]
        [Display(Name = "Nome do Gestor")]
        [MaxLength(100)]
        public string? Gestor { get; set; } = string.Empty;

        [Column("cnpj_auditor")]
        [Display(Name = "CNPJ do Auditor")]
        [MaxLength(18)]
        public string? CnpjAuditor { get; set; } = string.Empty;

        [Column("auditor")]
        [Display(Name = "Nome do Auditor")]
        [MaxLength(100)]
        public string? Auditor { get; set; } = string.Empty;

        [Column("cnpj_custodiante")]
        [Display(Name = "CNPJ do Custodiante")]
        [MaxLength(18)]
        public string? CnpjCustodiante { get; set; } = string.Empty;

        [Column("custodiante")]
        [Display(Name = "Nome do Custodiante")]
        [MaxLength(100)]
        public string? Custodiante { get; set; } = string.Empty;

        [Column("cnpj_controlador")]
        [Display(Name = "CNPJ do Controlador")]
        [MaxLength(18)]
        public string? CnpjControlador { get; set; } = string.Empty;

        [Column("controlador")]
        [Display(Name = "Nome do Controlador")]
        [MaxLength(100)]
        public string? Controlador { get; set; } = string.Empty;

        [Column("invest_cempr_exter")]
        [Display(Name = "Indica se o fundo pode aplicar 100% dos recursos no exterior")]
        [MaxLength(1)]
        public string? AplicarTotalExterior { get; set; }

        [Column("classe_anbima")]
        [Display(Name = "Classificação de Fundos regulados ANBIMA")]
        [MaxLength(100)]
        public string? ClasseAnbima { get; set; } = string.Empty;

        public Conteudo() { }
    }
}
