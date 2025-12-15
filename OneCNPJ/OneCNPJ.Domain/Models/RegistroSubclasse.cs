using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("registro_subclasse")]
    public class RegistroSubclasse 
        : BaseModel, IEntity
    {
        [ForeignKey("cadfi_id")]
        public long CadfiId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cadfi? Cadfi { get; set; }

        [ForeignKey("registro_classe_id")]
        public long? RegistroClasseId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual RegistroClasse? RegistroClasse { get; set; }

        [Column("cabecalho")]
        public List<string> Cabecalho { get; set; } = [];

        [Column("id_registro_classe")]
        public long IdRegistroClasse { get; set; }

        [Column("id_registro_subclasse")]
        public long IdRegistroSubclasse { get; set; }

        [Column("codigo_cvm")]
        [MaxLength(7)]
        public string? CodigoCvm { get; set; }

        [Column("data_constituicao")]
        public DateOnly? DataConstituicao { get; set; }

        [Column("data_inicio")]
        public DateOnly? DataInicio { get; set; }

        [Column("denominacao_social")]
        [MaxLength(100)]
        public string DenominacaoSocial { get; set; } = string.Empty;

        [Column("situacao")]
        [MaxLength(100)]
        public string? Situacao { get; set; } = string.Empty;

        [Column("data_inicio_situacao")]
        public DateOnly? DataInicioSituacao { get; set; }

        [Column("forma_condominio")]
        [MaxLength(100)]
        public string? FormaCondominio { get; set; } = string.Empty;

        [Column("exclusivo")]
        [MaxLength(1)]
        public string? Exclusivo { get; set; }

        [Column("publico_alvo")]
        [MaxLength(15)]
        public string? PublicoAlvo { get; set; } = string.Empty;

        public RegistroSubclasse() { }
    }
}
