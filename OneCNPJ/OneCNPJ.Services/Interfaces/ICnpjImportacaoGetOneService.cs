using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Interfaces
{
    public interface ICnpjImportacaoGetOneService
    {
        Task<CnpjImportacaoStatusVO?> GetStatusAsync();
        Task<CnpjImportacaoVO?> GetAtualAsync();
        Task<CnpjImportacaoVO?> GetUltimoAsync();
        Task<CnpjImportacaoVO?> GetPorCnpjAsync(string cnpj, string traceId);
    }
}