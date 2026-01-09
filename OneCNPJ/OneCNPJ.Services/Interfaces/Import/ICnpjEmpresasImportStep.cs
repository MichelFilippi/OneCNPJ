using OneCNPJ.DTOs.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface ICnpjEmpresasImportStep
    {
        Task<bool> ImportarAsync(CnpjImportacaoVO importacao, string traceId);
    }

}
