using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites
{
    public interface ILayoutCampoRepository
        : IBaseRepository<LayoutCampo, LayoutCampoVO, LayoutCampoListaVO>
    {
        Task<IEnumerable<LayoutCampoListaVO>> GetTodosOperacionaisPorLayoutIdAsync(long layoutId);
    }
}
