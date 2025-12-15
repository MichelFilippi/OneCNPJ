using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Services.Interfaces.Persistences;

namespace OneCNPJ.Services.Services.Persistences
{
    public class CadfiPersistService(
        ICadfiRepository repository,
        ILogger<CadfiPersistService> logger)
        : ICadfiPersistService
    {
        private readonly ICadfiRepository repository = repository;
        private readonly ILogger<CadfiPersistService> logger = logger;

        public async Task<CadfiVO> Create(CadfiVO entity)
        {
            try
            {
                logger.LogInformation("Criando registro CADFI.");
                var result = await repository.Create(entity);
                logger.LogInformation("Registro CADFI criado com sucesso. Id: {Id}", result.Id);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar registro CADFI.");
                throw;
            }
        }

        public async Task<bool> Delete(long entityId)
        {
            try
            {
                logger.LogInformation("Excluindo registro CADFI. Id: {Id}", entityId);
                var result = await repository.Delete(entityId);
                if (result)
                    logger.LogInformation("Registro CADFI excluído com sucesso. Id: {Id}", entityId);
                else
                    logger.LogWarning("Registro CADFI não encontrado para exclusão. Id: {Id}", entityId);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao excluir registro CADFI. Id: {Id}", entityId);
                throw;
            }
        }

        public async Task<CadfiVO> Update(CadfiVO entity)
        {
            try
            {
                logger.LogInformation("Atualizando registro CADFI. Id: {Id}", entity.Id);
                var result = await repository.Update(entity);
                logger.LogInformation("Registro CADFI atualizado com sucesso. Id: {Id}", result.Id);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar registro CADFI. Id: {Id}", entity.Id);
                throw;
            }
        }
    }
}