using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Services.Interfaces.GetOne;

namespace OneCNPJ.Services.Services.GetOne
{
    public class CadfiGetOneService(
        ICadfiRepository repository,
        ILogger<CadfiGetOneService> logger)
        : ICadfiGetOneService
    {
        private readonly ICadfiRepository repository = repository;
        private readonly ILogger<CadfiGetOneService> logger = logger;

        public async Task<CadfiStatusVO?> GetAtualAsync()
        {
            try
            {
                logger.LogInformation("Buscando registro CADFI atual.");
                var atual = await repository.GetAtualAsync();
                if (atual == null)
                    logger.LogWarning("Nenhum registro CADFI atual encontrado.");
                else
                    logger.LogInformation("Registro CADFI atual encontrado. Id: {Id}", atual.Id);
                return atual?.ToStatus();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro CADFI atual.");
                throw;
            }
        }

        public async Task<CadfiVO?> GetRegistroPorHashAsync(string hash)
        {
            try
            {
                logger.LogInformation("Buscando registro CADFI por hash: {Hash}", hash);
                var result = await repository.GetRegistroPorHashAsync(hash);
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado para hash: {Hash}", hash);
                else
                    logger.LogInformation("Registro CADFI encontrado para hash: {Hash}. Id: {Id}", hash, result.Id);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro CADFI por hash: {Hash}", hash);
                throw;
            }
        }

        public async Task<CadfiVO?> GetRegistroPorIdAsync(long entityId)
        {
            try
            {
                logger.LogInformation("Buscando registro CADFI por Id: {Id}", entityId);
                var result = await repository.GetRegistroPorIdAsync(entityId);
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado para Id: {Id}", entityId);
                else
                    logger.LogInformation("Registro CADFI encontrado para Id: {Id}", entityId);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro CADFI por Id: {Id}", entityId);
                throw;
            }
        }

        public async Task<CadfiStatusVO?> GetStatusAsync()
        {
            try
            {
                logger.LogInformation("Buscando status do último registro CADFI.");
                var atual = await repository.GetUltimoAsync();
                if (atual == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado ao buscar status.");
                else
                    logger.LogInformation("Status do último registro CADFI encontrado. Id: {Id}", atual.Id);
                return atual?.ToStatus();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar status do último registro CADFI.");
                throw;
            }
        }

        public async Task<CadfiVO?> GetUltimoAsync()
        {
            try
            {
                logger.LogInformation("Buscando o último registro CADFI.");
                var result = await repository.GetUltimoAsync();
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado ao buscar o último registro.");
                else
                    logger.LogInformation("Último registro CADFI encontrado. Id: {Id}", result.Id);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar o último registro CADFI.");
                throw;
            }
        }
    }
}