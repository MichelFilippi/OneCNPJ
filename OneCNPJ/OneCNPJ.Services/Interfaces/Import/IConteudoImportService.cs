using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface IConteudoImportService
    {
        Task<bool> ImportarDaCvmAsync(CadfiVO cadfiEntity);
    }
}
