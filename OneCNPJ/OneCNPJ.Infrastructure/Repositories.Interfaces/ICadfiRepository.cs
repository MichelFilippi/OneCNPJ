using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface ICadfiRepository
        : IBaseRepository<Cadfi, CadfiVO, CadfiListaVO>
    {
        Task<IEnumerable<CadfiListaVO>> GetTodosNaoOperacionaisAsync();
        Task<IEnumerable<CadfiListaVO>> GetTodosOperacionaisAsync();
        Task<IEnumerable<CadfiListaVO>> GetTodosProcessandoAsync();
        Task<IEnumerable<CadfiListaVO>> GetUltimosAsync(int rows);
        Task<CadfiVO?> GetRegistroPorHashAsync(string hash);
        Task<CadfiVO?> GetUltimoAsync();
        Task<CadfiVO?> GetAtualAsync();
    }
}
