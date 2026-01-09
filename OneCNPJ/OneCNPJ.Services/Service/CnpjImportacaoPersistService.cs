using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces;

namespace OneCNPJ.Services.Persistences
{
    public class CnpjImportacaoPersistService(
           ICnpjImportacaoRepository repository,
           ILogger<CnpjImportacaoPersistService> logger)
           : ICnpjImportacaoPersistService
    {
        private readonly ICnpjImportacaoRepository _repository = repository;
        private readonly ILogger<CnpjImportacaoPersistService> _logger = logger;

        public async Task<CnpjImportacaoVO> Create(CnpjImportacaoVO entity)
        {
            _logger.LogInformation("Criando CnpjImportacao.");
            return await _repository.Create(entity);
        }

        public async Task<CnpjImportacaoVO> Update(CnpjImportacaoVO entity)
        {
            _logger.LogInformation("Atualizando CnpjImportacao. Id: {Id}", entity.Id);
            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(long entityId)
        {
            _logger.LogInformation("Excluindo CnpjImportacao. Id: {Id}", entityId);
            return await _repository.Delete(entityId);
        }
    }
}
