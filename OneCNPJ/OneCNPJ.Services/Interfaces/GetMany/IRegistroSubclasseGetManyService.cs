using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Services.Interfaces.GetMany
{
    public interface IRegistroSubclasseGetManyService
    {
        Task<IEnumerable<RegistroSubclasseListaVO>> GetTodosOperacionaisPorIdRegistroClasseECadfiIdAsync(long idRegistroClasse, long cadfiId);
        Task<IEnumerable<RegistroSubclasseListaVO>> GetTodosOperacionaisPorRegistroClasseIdECadfiIdAsync(long registroClasseId, long cadfiId);
    }
}
