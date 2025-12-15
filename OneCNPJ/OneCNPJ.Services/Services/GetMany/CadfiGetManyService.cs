using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Services.Interfaces.GetMany;

namespace OneCNPJ.Services.Services.GetMany
{
    public class CadfiGetManyService(
        ICadfiRepository repository,
        ILogger<CadfiGetManyService> logger)
        : ICadfiGetManyService
    {
        private readonly ICadfiRepository repository = repository;
        private readonly ILogger<CadfiGetManyService> logger = logger;

        public async Task<IEnumerable<CadfiListaVO>> GetTodosNaoOperacionaisAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os registros CADFI não operacionais.");
                var result = await repository.GetTodosNaoOperacionaisAsync();
                if (!result.Any())
                    { logger.LogWarning("Nenhum registro CADFI não operacional encontrado."); }
                else
                    { logger.LogInformation("Encontrados {Count} registros CADFI não operacionais.", result.Count()); }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registros CADFI não operacionais.");
                throw;
            }
        }

        public async Task<IEnumerable<CadfiListaVO>> GetTodosOperacionaisAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os registros CADFI operacionais.");
                var result = await repository.GetTodosOperacionaisAsync();
                if (!result.Any())
                    { logger.LogWarning("Nenhum registro CADFI operacional encontrado."); }
                else
                    { logger.LogInformation("Encontrados {Count} registros CADFI operacionais.", result.Count()); }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registros CADFI operacionais.");
                throw;
            }
        }

        public async Task<IEnumerable<CadfiListaVO>> GetTodosProcessandoAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os registros CADFI em processamento.");
                var result = await repository.GetTodosProcessandoAsync();
                if (!result.Any())
                    { logger.LogWarning("Nenhum registro CADFI em processamento encontrado."); }
                else
                    { logger.LogInformation("Encontrados {Count} registros CADFI em processamento.", result.Count()); }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registros CADFI em processamento.");
                throw;
            }
        }

        public async Task<IEnumerable<CadfiListaVO>> GetUltimosAsync(int rows)
        {
            try
            {
                logger.LogInformation("Buscando os {Rows} últimos registros CADFI.", rows);
                var result = await repository.GetUltimosAsync(rows);
                if (!result.Any())
                    { logger.LogWarning("Nenhum registro CADFI encontrado ao buscar os {Rows} últimos.", rows); }
                else
                    { logger.LogInformation("Encontrados {Count} registros CADFI ao buscar os {Rows} últimos.", result.Count(), rows); }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar os {Rows} últimos registros CADFI.", rows);
                throw;
            }
        }
    }
}