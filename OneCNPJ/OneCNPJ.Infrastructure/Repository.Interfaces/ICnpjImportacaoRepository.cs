using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repository.Interfaces
{
    public interface ICnpjImportacaoRepository
        : IBaseRepository<OneCNPJ.Domain.Models.CnpjImportacao, CnpjImportacaoVO, CnpjImportacaoListaVO>
    {
        Task<CnpjImportacaoVO?> GetAtualAsync();
        Task<CnpjImportacaoVO?> GetUltimoAsync();
        Task<IEnumerable<CnpjImportacaoListaVO>> GetUltimosAsync(int rows);

        Task<IEnumerable<CnpjImportacaoListaVO>> GetTodosProcessandoAsync();
        Task<IEnumerable<CnpjImportacaoListaVO>> GetTodosOkAsync();

        Task<CnpjImportacaoVO?> GetRegistroPorCnpjAsync(string cnpj14, string traceId);
    }
}