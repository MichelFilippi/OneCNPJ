using OneCNPJ.Common.Enums;

namespace OneCNPJ.DTOs
{
    public class BaseVO
    {
        public long Id { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Ativo;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
    }
}
