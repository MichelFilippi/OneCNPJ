using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.Services.Interfaces.GetOne
{
    public interface ICadfiGetOneService
    {
        Task<CadfiVO?> GetRegistroPorIdAsync(long entityId);
        Task<CadfiVO?> GetRegistroPorHashAsync(string hash);
        Task<CadfiVO?> GetUltimoAsync();
        Task<CadfiStatusVO?> GetStatusAsync();
        Task<CadfiStatusVO?> GetAtualAsync();
    }
}
