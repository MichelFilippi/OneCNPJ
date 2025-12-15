using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Services.Interfaces.GetMany
{
    public interface IRegistroClasseGetManyService
    {
        Task<IEnumerable<RegistroClasseListaVO>> GetTodosOperacionaisPorIdRegistroFundoECadfiIdAsync(long idRegistroFundo, long cadfiId);
        Task<IEnumerable<RegistroClasseListaVO>> GetTodosOperacionaisPorRegistroFundoIdECadfiIdAsync(long registroFundoId, long cadfiId);
    }
}
