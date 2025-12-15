using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class FalhaRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<FalhaRepository> logger)
        : BaseRepository<Falha, FalhaVO, FalhaListaVO>(contextFactory, logger),
        IFalhaRepository
    {
        private readonly ILogger<FalhaRepository> logger = logger;

        public async Task<IEnumerable<FalhaListaVO>> GetTodosPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as falhas para CadfiId: {CadfiId}", cadfiId);
                return await GetListDetailsAsync(
                    context => context.Falhas
                        .Where(p => p.CadfiId == cadfiId)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar falhas para CadfiId: {CadfiId}", cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(Falha ent)
        {
            return Task.CompletedTask;
        }
    }
}