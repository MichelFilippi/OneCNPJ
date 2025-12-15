using OneCNPJ.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain
{
    public class BaseModel
    {
        [Key]
        [Column("id")]
        [Required]
        public long Id { get; set; }

        [Column("status")]
        public StatusEnum Status { get; set; } = StatusEnum.Processamento;

        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Column("data_atualizacao")]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
    }
}
