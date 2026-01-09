using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces;

namespace OneCNPJ.Services.Services
{
    public class CnpjImportacaoGetOneService(
        ICnpjImportacaoRepository repository,
        ILogger<CnpjImportacaoGetOneService> logger)
        : ICnpjImportacaoGetOneService
    {
        private readonly ICnpjImportacaoRepository _repository = repository;
        private readonly ILogger<CnpjImportacaoGetOneService> _logger = logger;

        public async Task<CnpjImportacaoVO?> GetAtualAsync()
        {
            _logger.LogInformation("Buscando importação atual (CNPJ).");
            return await _repository.GetAtualAsync();
        }

        public async Task<CnpjImportacaoVO?> GetUltimoAsync()
        {
            _logger.LogInformation("Buscando última importação (CNPJ).");
            return await _repository.GetUltimoAsync();
        }

        public async Task<CnpjImportacaoStatusVO?> GetStatusAsync()
        {
            try
            {
                _logger.LogInformation("Buscando status da última importação (CNPJ).");
                var atual = await _repository.GetUltimoAsync();

                if (atual == null)
                    _logger.LogWarning("Nenhuma importação CNPJ encontrada ao buscar status.");
                else
                    _logger.LogInformation("Status da última importação CNPJ encontrado. Id: {Id}", atual.Id);

                return atual?.ToStatus();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar status da última importação CNPJ.");
                throw;
            }
        }

        public async Task<CnpjImportacaoVO?> GetPorCnpjAsync(string cnpj, string traceId)
        {
            _logger.LogInformation("Buscando importação + dados por CNPJ. TraceId: {TraceId} {Cnpj}", traceId, cnpj);
            return await _repository.GetRegistroPorCnpjAsync(cnpj, traceId);
        }
    }
}