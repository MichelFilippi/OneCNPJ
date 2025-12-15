using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites;

namespace OneCNPJ.Infrastructure.Repositories.Satelites
{
    public class LayoutCampoRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<LayoutCampoRepository> logger)
        : BaseRepository<LayoutCampo, LayoutCampoVO, LayoutCampoListaVO>(contextFactory, logger),
        ILayoutCampoRepository
    {
        private readonly ILogger<LayoutCampoRepository> logger = logger;

        public async Task<IEnumerable<LayoutCampoListaVO>> GetTodosOperacionaisPorLayoutIdAsync(long layoutId)
        {
            try
            {
                logger.LogInformation("Buscando todos os campos operacionais para LayoutId: {LayoutId}", layoutId);
                return await GetListDetailsAsync(
                    context => context.LayoutCampos
                        .Where(p => p.LayoutId == layoutId && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar campos operacionais para LayoutId: {LayoutId}", layoutId);
                throw;
            }
        }

        protected override Task FillInDetails(LayoutCampo ent)
        {
            return Task.CompletedTask;
        }
    }
}