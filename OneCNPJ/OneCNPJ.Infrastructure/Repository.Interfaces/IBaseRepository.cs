using OneCNPJ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Infrastructure.Repository.Interfaces
{
    public interface IBaseRepository<TEntity, TEntityVO, TEntityListVO>
        where TEntity : class
        where TEntityVO : class
        where TEntityListVO : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntityVO> Create(TEntityVO entity);
        Task<TEntityVO> Update(TEntityVO entity);
        Task BulkInsertAsync(IEnumerable<TEntityVO> entities);
        Task BulkUpdateAsync(IEnumerable<TEntityVO> entities);
        Task<TEntityVO?> GetRegistroPorIdAsync(long entityId);
        Task<IEnumerable<TEntityVO>> GetDetailsAsync(Func<ApplicationDbContext, IQueryable<TEntity>> query);
        Task<IEnumerable<TEntityListVO>> GetListDetailsAsync(Func<ApplicationDbContext, IQueryable<TEntity>> query);
        Task<TEntityVO?> GetDetailAsync(Func<ApplicationDbContext, Task<TEntity?>> query);
        Task<bool> Delete(long entityId);
    }
}
