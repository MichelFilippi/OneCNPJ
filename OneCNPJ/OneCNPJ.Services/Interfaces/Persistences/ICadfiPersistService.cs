using OneCNPJ.DTOs.VOs;

namespace OneCNPJ.Services.Interfaces.Persistences
{
    public interface ICadfiPersistService
    {
        Task<CadfiVO> Create(CadfiVO entity);
        Task<CadfiVO> Update(CadfiVO entity);
        Task<bool> Delete(long entityId);
    }
}
