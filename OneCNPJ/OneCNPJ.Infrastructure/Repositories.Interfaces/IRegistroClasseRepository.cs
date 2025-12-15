using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IRegistroClasseRepository
        : IBaseRepository<RegistroClasse, RegistroClasseVO, RegistroClasseListaVO>
    {
        Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorIdRegistroFundoECadfiIdAsync(long idRegistroFundo, long cadfiId);
        Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorRegistroFundoIdECadfiIdAsync(long registroFundoId, long cadfiId);
        Task<IEnumerable<RegistroClasseVO>> GetTodosPorCadfiIdAsync(long cadfiId);
        Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorCadfiIdAsync(long cadfiId);

    }
}
