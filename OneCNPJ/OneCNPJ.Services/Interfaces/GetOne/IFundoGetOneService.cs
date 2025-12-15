using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.Services.Interfaces.GetOne
{
    public interface IFundoGetOneService
    {
        Task<FundoVO?> GetRegistroPorCnpjAsync(string cnpjFundo, string traceId);
    }
}
