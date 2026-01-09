using OneCNPJ.DTOs.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Services.Interfaces.Import
{
    public interface ICnpjImportService
    {
        Task<CnpjImportacaoVO?> ImportarDaRfbAsync(string traceId);
    }
}