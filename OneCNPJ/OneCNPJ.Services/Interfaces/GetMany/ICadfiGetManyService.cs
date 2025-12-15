using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Services.Interfaces.GetMany
{
    public interface ICadfiGetManyService
    {
        Task<IEnumerable<CadfiListaVO>> GetTodosNaoOperacionaisAsync();
        Task<IEnumerable<CadfiListaVO>> GetTodosOperacionaisAsync();
        Task<IEnumerable<CadfiListaVO>> GetTodosProcessandoAsync();
        Task<IEnumerable<CadfiListaVO>> GetUltimosAsync(int rows);

    }
}
