using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("registro_fundo")]
    public class RegistroFundo 
        : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [Column("cabecalho")]
        public List<string> Cabecalho { get; set; } = [];

        [Column("id_registro_fundo")]
        public long IdRegistroFundo { get; set; }

        [Column("cnpj_fundo")]
        [MaxLength(18)]
        public string CnpjFundo { get; set; } = string.Empty;

        [Column("codigo_cvm")]
        [MaxLength(7)]
        public string? CodigoCvm { get; set; }

        [Column("data_registro")]
        public DateOnly? DataRegistro { get; set; }

        [Column("data_constituição")]
        public DateOnly? DataConstituicao { get; set; }

        [Column("tipo_fundo")]
        [MaxLength(20)]
        public string TipoFundo { get; set; } = string.Empty;

        [Column("denominacao_social")]
        [MaxLength(100)]
        public string DenominacaoSocial { get; set; } = string.Empty;

        [Column("data_cancelamento")]
        public DateOnly? DataCancelamento { get; set; }

        [Column("situacao")]
        [MaxLength(100)]
        public string? Situacao { get; set; } = string.Empty;

        [Column("data_inicio_situacao")]
        public DateOnly? DataInicioSituacao { get; set; }

        [Column("data_adaptacao_rcvm_175")]
        public DateOnly? DataAdaptacaoRcvm175 { get; set; }

        [Column("data_inicio_exercicio_social")]
        public DateOnly? DataInicioExercicioSocial { get; set; }

        [Column("data_fim_exercicio_social")]
        public DateOnly? DataFimExercicioSocial { get; set; }

        [Column("patrimonio_liquido")]
        [Precision(26, 2)]
        public decimal PatrimonioLiquido { get; set; } = 0;

        [Column("data_patrimonio_liquido")]
        public DateOnly? DataPatrimonioLiquido { get; set; }

        [Column("diretor")]
        [MaxLength(100)]
        public string? Diretor { get; set; } = string.Empty;

        [Column("cnpj_administrador")]
        [MaxLength(18)]
        public string? CnpjAdministrador { get; set; } = string.Empty;

        [Column("administrador")]
        [MaxLength(100)]
        public string? Administrador { get; set; } = string.Empty;

        [Column("tipo_pessoa_gestor")]
        [MaxLength(2)]
        public string? TipoPessoaGestor { get; set; }

        [Column("documento_gestor")]
        [MaxLength(18)]
        public string? DocumentoGestor { get; set; } = string.Empty;

        [Column("gestor")]
        [MaxLength(100)]
        public string? Gestor { get; set; } = string.Empty;

        public RegistroFundo() { }
    }
}
