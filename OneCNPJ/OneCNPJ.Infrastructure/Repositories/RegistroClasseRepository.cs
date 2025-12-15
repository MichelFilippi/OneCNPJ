using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class RegistroClasseRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<RegistroClasseRepository> logger)
        : BaseRepository<RegistroClasse, RegistroClasseVO, RegistroClasseListaVO>(contextFactory, logger),
        IRegistroClasseRepository
    {
        private readonly ILogger<RegistroClasseRepository> logger = logger;

        public async Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as classes operacionais para CadfiId: {cadfiId}", cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroClasses
                        .Include(c => c.RegistroFundo)
                        .Where(p => p.RegistroFundo!.CadfiId == cadfiId && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar classes operacionais para CadfiId: {cadfiId}", cadfiId);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorIdRegistroFundoECadfiIdAsync(long idRegistroFundo, long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as classes operacionais para IdRegistroFundo: {idRegistroFundo} e CadfiId: {cadfiId}", idRegistroFundo, cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroClasses
                        .Where(p => p.IdRegistroFundo == idRegistroFundo 
                            && p.CadfiId == cadfiId
                            && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar classes operacionais para IdRegistroFundo: {idRegistroFundo} e CadfiId: {cadfiId}", idRegistroFundo, cadfiId);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroClasseVO>> GetTodosOperacionaisPorRegistroFundoIdECadfiIdAsync(long registroFundoId, long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as classes operacionais para RegistroFundoId: {registroFundoId} e CadfiId: {cadfiId}", registroFundoId, cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroClasses
                        .Where(p => p.RegistroFundoId == registroFundoId 
                            && p.CadfiId == cadfiId
                            && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar classes operacionais para RegistroFundoId: {RegistroFundoId} e CadfiId: {cadfiId}", registroFundoId, cadfiId);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroClasseVO>> GetTodosPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as classes para CadfiId: {CadfiId}", cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroClasses
                        .Include(c => c.RegistroFundo)
                        .Where(p => p.RegistroFundo!.CadfiId == cadfiId)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar classes para CadfiId: {CadfiId}", cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(RegistroClasse ent)
        {
            return Task.CompletedTask;
        }
    }
}