using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.GetMany
{
    public interface IRegistroFundoGetManyService
    {
        Task<IEnumerable<RegistroFundoVO>> GetTodosPorCadfiIdAsync(long cadfiId);
        Task<IEnumerable<RegistroFundoVO>> GetTodosOperacionaisPorCadfiIdAsync(long cadfiId);
    }
}
