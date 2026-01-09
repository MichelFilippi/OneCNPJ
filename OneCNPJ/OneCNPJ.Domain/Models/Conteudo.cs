using System.ComponentModel.DataAnnotations.Schema;
using OneCNPJ.Common.Enums;

namespace OneCNPJ.Domain.Models
{
    [Table("cnpj_conteudo")]
    public class Conteudo : BaseModel, IEntity
    {
        [Column("importacao_id")]
        public long ImportacaoId { get; set; }

        [ForeignKey(nameof(ImportacaoId))]
        public virtual CnpjImportacao? Importacao { get; set; }

        // CNPJ completo 14 dígitos (basico+ordem+dv)
        [Column("cnpj")]
        public string Cnpj { get; set; } = string.Empty;

        // cache do JSON final
        // Migração: defina como jsonb
        [Column("payload_json")]
        public string PayloadJson { get; set; } = "{}";

        [Column("status")]
        public StatusEnum Status { get; set; } = StatusEnum.Processamento;

        public Conteudo() { }
    }
}
