using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface IRegistroClasseImportService
    {
        Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity);
    }
}
