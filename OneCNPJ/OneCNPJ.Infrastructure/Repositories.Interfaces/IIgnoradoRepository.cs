using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IIgnoradoRepository
        : IBaseRepository<Ignorado, IgnoradoVO, IgnoradoListaVO>
    {
        Task<IEnumerable<IgnoradoListaVO>> GetTodosPorCadfiIdAsync(long cadfiId);
    }
}
