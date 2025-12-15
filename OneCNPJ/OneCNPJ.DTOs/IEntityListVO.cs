using OneCNPJ.Common.Enums;

namespace OneCNPJ.DTOs
{
    public interface IEntityListVO
    {
        long Id { get; set; }
        StatusEnum Status { get; set; }
        DateTime DataCriacao { get; set; }
        DateTime DataAtualizacao { get; set; }
    }
}
