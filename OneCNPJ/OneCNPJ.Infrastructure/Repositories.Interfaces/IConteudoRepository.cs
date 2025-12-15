using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.Infrastructure.Repositories.Interfaces
{
    public interface IConteudoRepository
        : IBaseRepository<Conteudo, ConteudoVO, ConteudoListaVO>
    {
        Task<ConteudoVO?> GetRegistroPorCnpjFundoAsync(string cnpjFundo);
        Task<ConteudoVO?> GetRegistroPorCnpjFundoECadfiIdAsync(string cnpjFundo, long cadfiId);
    }
}
