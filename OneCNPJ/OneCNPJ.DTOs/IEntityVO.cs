using OneCNPJ.Common.Enums;

namespace OneCNPJ.DTOs
{
    public interface IEntityVO<TEntity, TEntityVO, TEntityListVO>
    {
        long Id { get; set; }
        StatusEnum Status { get; set; }
        DateTime DataCriacao { get; set; }
        DateTime DataAtualizacao { get; set; }
        TEntity ToDomain();
        TEntityVO FromDomain(TEntity entity);
        TEntityListVO ListFromDomain(TEntity entity);
    }
}
