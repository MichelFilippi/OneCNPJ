using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpressoData.Domain.Warehouse
{
    public interface IWarehouseRepository
    {
        Task<Deposito> AddAsync(Deposito item);
        Task<Deposito?> GetByIdAsync(int id);
        Task<IEnumerable<Deposito>> GetAllAsync();
        Task<Deposito> UpdateAsync(Deposito item);
        Task<bool> DeleteAsync(int id);
    }
}