using OneCNPJ.Common.Enums;

namespace OneCNPJ.Domain
{
    public interface IEntity
    {
        long Id { get; set; }
        StatusEnum Status { get; set; }
    }
}
