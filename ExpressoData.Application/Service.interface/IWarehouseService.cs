using ExpressoData.Domain.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpressoData.Application.Warehouse
{
    public interface IWarehouseService
    {
        Task<Deposito> AddAsync(Deposito item);
        Task<Deposito?> GetByIdAsync(int id);
        Task<IEnumerable<Deposito>> GetAllAsync();
        Task<Deposito> UpdateAsync(Deposito item);
        Task<bool> DeleteAsync(int id);
    }
}