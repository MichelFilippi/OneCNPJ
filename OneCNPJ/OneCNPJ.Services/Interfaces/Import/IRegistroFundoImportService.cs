using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface IRegistroFundoImportService
    {
        Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity);
    }
}
