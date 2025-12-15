using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface ICadfiImportService
    {
        Task<CadfiVO?> ImportarDaCvmAsync(string traceId);
    }
}
