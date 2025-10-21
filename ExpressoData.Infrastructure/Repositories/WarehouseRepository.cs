using ExpressoData.Domain.Warehouse;
using ExpressoData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressoData.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ExpressoDbContext _context;
        public WarehouseRepository(ExpressoDbContext context)
        {
            _context = context;
        }
        public async Task<Deposito> AddAsync(Deposito item)
        {
            _context.Depositos.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Deposito?> GetByIdAsync(int id)
        {
            return await _context.Depositos.FindAsync(id);
        }

        public async Task<IEnumerable<Deposito>> GetAllAsync()
        {
            return await _context.Depositos.ToListAsync();
        }

        public async Task<Deposito> UpdateAsync(Deposito item)
        {
            _context.Depositos.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Depositos.FindAsync(id);
            if (item == null) return false;
            _context.Depositos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}