
using ExpressoData.Domain.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpressoData.Application.Warehouse
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;
        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public Task<Deposito> AddAsync(Deposito item) => _repository.AddAsync(item);
        public Task<Deposito?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<IEnumerable<Deposito>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Deposito> UpdateAsync(Deposito item) => _repository.UpdateAsync(item);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}