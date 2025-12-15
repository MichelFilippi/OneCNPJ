using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class CadfiRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<CadfiRepository> logger)
        : BaseRepository<Cadfi, CadfiVO, CadfiListaVO>(contextFactory, logger),
        ICadfiRepository
    {
        private readonly ILogger<CadfiRepository> logger = logger;

        public async Task<CadfiVO?> GetAtualAsync()
        {
            try
            {
                logger.LogInformation("Buscando registro CADFI atual (Status == 502).");
                var result = await GetDetailAsync(
                    context => context.Cadfis
                        .Where(p => (int)p.Status == 502)
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI atual encontrado (Status == 502).");
                return result;
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
                var result = await GetDetailAsync(
                    context => context.Cadfis
                        .Where(p => p.Hash.Contains(hash))
                        .FirstOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado para hash: {Hash}", hash);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro CADFI por hash: {Hash}", hash);
                throw;
            }
        }

        public async Task<IEnumerable<CadfiListaVO>> GetTodosNaoOperacionaisAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os registros CADFI não operacionais (Status < 0).");
                var result = await GetListDetailsAsync(
                    context => context.Cadfis
                        .Where(p => p.Status < 0)
                );
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
                logger.LogInformation("Buscando todos os registros CADFI operacionais (Status >= 500).");
                var result = await GetListDetailsAsync(
                    context => context.Cadfis
                        .Where(p => (int)p.Status >= 500)
                );
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
                logger.LogInformation("Buscando todos os registros CADFI em processamento (Status > 0 && Status < 500).");
                var result = await GetListDetailsAsync(
                    context => context.Cadfis
                        .Where(p => (int)p.Status > 0 && (int)p.Status < 500)
                );
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registros CADFI em processamento.");
                throw;
            }
        }

        public async Task<CadfiVO?> GetUltimoAsync()
        {
            try
            {
                logger.LogInformation("Buscando o último registro CADFI.");
                var result = await GetDetailAsync(
                    context => context.Cadfis
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum registro CADFI encontrado ao buscar o último registro.");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar o último registro CADFI.");
                throw;
            }
        }

        public async Task<IEnumerable<CadfiListaVO>> GetUltimosAsync(int rows)
        {
            try
            {
                logger.LogInformation("Buscando os {Rows} últimos registros CADFI (Status >= 500 && Status < 1000).", rows);
                var result = await GetListDetailsAsync(
                    context => context.Cadfis
                        .Where(p => (int)p.Status >= 500 && (int)p.Status < 1000)
                        .OrderByDescending(p => p.Id)
                        .Take(rows)
                );
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar os {Rows} últimos registros CADFI.", rows);
                throw;
            }
        }

        protected override Task FillInDetails(Cadfi ent)
        {
            return Task.CompletedTask;
        }
    }
}