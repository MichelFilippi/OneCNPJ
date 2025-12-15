using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface IRegistroSubclasseImportService
    {
        Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity);
    }
}
