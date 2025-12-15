using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites
{
    public interface ILayoutRepository
        : IBaseRepository<Layout, LayoutVO, LayoutListaVO>
    {
        Task<IEnumerable<LayoutListaVO>> GetTodosNaoOperacionaisAsync();
        Task<IEnumerable<LayoutListaVO>> GetTodosOperacionaisAsync();
        Task<IEnumerable<LayoutListaVO>> GetTodosProcessandoAsync();
        Task<IEnumerable<LayoutListaVO>> GetUltimosAsync(int rows);
        Task<IEnumerable<LayoutVO>> GetTodosOperacionaisCompletoAsync();
        Task<IEnumerable<LayoutVO>> GetTodosOperacionaisPorFormatoCadfiAsync(FormatoCadfiEnum formatoCadfi);
    }
}
