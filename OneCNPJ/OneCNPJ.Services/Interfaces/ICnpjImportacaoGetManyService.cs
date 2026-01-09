using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Interfaces
{
    public interface ICnpjImportacaoGetManyService
    {
        Task<IEnumerable<CnpjImportacaoListaVO>> GetUltimosAsync(int rows);
    }
}