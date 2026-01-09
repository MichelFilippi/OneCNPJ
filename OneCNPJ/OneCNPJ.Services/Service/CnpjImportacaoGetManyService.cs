using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces;

namespace OneCNPJ.Services.Services.GetMany
{
    public class CnpjImportacaoGetManyService(
         ICnpjImportacaoRepository repository,
         ILogger<CnpjImportacaoGetManyService> logger)
         : ICnpjImportacaoGetManyService
    {
        private readonly ICnpjImportacaoRepository _repository = repository;
        private readonly ILogger<CnpjImportacaoGetManyService> _logger = logger;

        public async Task<IEnumerable<CnpjImportacaoListaVO>> GetUltimosAsync(int rows)
        {
            _logger.LogInformation("Buscando as {Rows} últimas importações (CNPJ).", rows);
            return await _repository.GetUltimosAsync(rows);
        }
    }
}