using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IRegistroSubclasseRepository
        : IBaseRepository<RegistroSubclasse, RegistroSubclasseVO, RegistroSubclasseListaVO>
    {
        Task<IEnumerable<RegistroSubclasseVO>> GetTodosOperacionaisPorIdRegistroClasseECadfiIdAsync(long idRegistroClasse, long cadfiId);
        Task<IEnumerable<RegistroSubclasseVO>> GetTodosOperacionaisPorRegistroClasseIdECadfiIdAsync(long registroClasseId, long cadfiId);
    }
}
