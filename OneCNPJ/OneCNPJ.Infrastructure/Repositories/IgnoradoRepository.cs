using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class IgnoradoRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<IgnoradoRepository> logger)
        : BaseRepository<Ignorado, IgnoradoVO, IgnoradoListaVO>(contextFactory, logger),
        IIgnoradoRepository
    {
        private readonly ILogger<IgnoradoRepository> logger = logger;

        public async Task<IEnumerable<IgnoradoListaVO>> GetTodosPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todos os ignorados para CadfiId: {CadfiId}", cadfiId);
                return await GetListDetailsAsync(
                    context => context.Ignorados
                        .Where(p => p.CadfiId == cadfiId)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar ignorados para CadfiId: {CadfiId}", cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(Ignorado ent)
        {
            return Task.CompletedTask;
        }
    }
}