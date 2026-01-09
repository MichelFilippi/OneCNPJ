using OneCNPJ.DTOs.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Interfaces
{
    public interface ICnpjImportacaoPersistService
    {
        Task<CnpjImportacaoVO> Create(CnpjImportacaoVO entity);
        Task<CnpjImportacaoVO> Update(CnpjImportacaoVO entity);
        Task<bool> Delete(long entityId);
    }
}
