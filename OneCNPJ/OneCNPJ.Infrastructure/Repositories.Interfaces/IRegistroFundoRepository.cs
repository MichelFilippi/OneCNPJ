using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IRegistroFundoRepository
        : IBaseRepository<RegistroFundo, RegistroFundoVO, RegistroFundoListaVO>
    {
        Task<IEnumerable<RegistroFundoVO>> GetTodosPorCadfiIdAsync(long cadfiId);
        Task<IEnumerable<RegistroFundoVO>> GetTodosOperacionaisPorCadfiIdAsync(long cadfiId);
        Task<RegistroFundoVO?> GetRegistroPorCnpjFundoAsync(string cnpjFundo);
        Task<RegistroFundoVO?> GetRegistroPorCnpjFundoECadfiIdAsync(string cnpjFundo, long cadfiId);
    }
}
