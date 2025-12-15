using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IFalhaRepository
        : IBaseRepository<Falha, FalhaVO, FalhaListaVO>
    {
        Task<IEnumerable<FalhaListaVO>> GetTodosPorCadfiIdAsync(long cadfiId);
    }
}
