using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces.Satelites;

namespace OneCNPJ.Infrastructure.Repositories.Satelites
{
    public class LayoutRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<LayoutRepository> logger)
        : BaseRepository<Layout, LayoutVO, LayoutListaVO>(contextFactory, logger),
        ILayoutRepository
    {
        private readonly ILogger<LayoutRepository> logger = logger;

        public async Task<IEnumerable<LayoutListaVO>> GetTodosNaoOperacionaisAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os layouts não operacionais (Status < 0).");
                return await GetListDetailsAsync(
                    context => context.Layouts
                        .Where(p => p.Status < 0)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar layouts não operacionais.");
                throw;
            }
        }

        public async Task<IEnumerable<LayoutListaVO>> GetTodosOperacionaisAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os layouts operacionais (Status >= 500).");
                return await GetListDetailsAsync(
                    context => context.Layouts
                        .Where(p => (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar layouts operacionais.");
                throw;
            }
        }

        public async Task<IEnumerable<LayoutVO>> GetTodosOperacionaisCompletoAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os layouts operacionais completos (Status >= 500).");
                return await GetDetailsAsync(
                    context => context.Layouts
                        .Include(p => p.Campos)
                        .Where(p => (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar layouts operacionais completos.");
                throw;
            }
        }

        public async Task<IEnumerable<LayoutVO>> GetTodosOperacionaisPorFormatoCadfiAsync(FormatoCadfiEnum formatoCadfi)
        {
            try
            {
                logger.LogInformation("Buscando todos os layouts operacionais para o formato {FormatoCadfi}.", formatoCadfi);
                return await GetDetailsAsync(
                    context => context.Layouts
                        .Include(p => p.Campos)
                        .Where(p => (int)p.Status >= 500 && p.FormatoCadfiEnum == formatoCadfi)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar layouts operacionais para o formato {FormatoCadfi}.", formatoCadfi);
                throw;
            }
        }

        public async Task<IEnumerable<LayoutListaVO>> GetTodosProcessandoAsync()
        {
            try
            {
                logger.LogInformation("Buscando todos os layouts em processamento (Status > 0 && Status < 500).");
                return await GetListDetailsAsync(
                    context => context.Layouts
                        .Where(p => (int)p.Status > 0 && (int)p.Status < 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar layouts em processamento.");
                throw;
            }
        }

        public async Task<IEnumerable<LayoutListaVO>> GetUltimosAsync(int rows)
        {
            try
            {
                logger.LogInformation("Buscando os {Rows} últimos layouts (Status >= 500 && Status < 1000).", rows);
                return await GetListDetailsAsync(
                    context => context.Layouts
                        .Where(p => (int)p.Status >= 500 && (int)p.Status < 1000)
                        .OrderByDescending(p => p.Id)
                        .Take(rows)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar os {Rows} últimos layouts.", rows);
                throw;
            }
        }

        protected override Task FillInDetails(Layout ent)
        {
            return Task.CompletedTask;
        }
    }
}