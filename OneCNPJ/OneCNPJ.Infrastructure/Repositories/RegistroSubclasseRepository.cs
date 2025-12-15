using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class RegistroSubclasseRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<RegistroSubclasseRepository> logger)
        : BaseRepository<RegistroSubclasse, RegistroSubclasseVO, RegistroSubclasseListaVO>(contextFactory, logger),
        IRegistroSubclasseRepository
    {
        private readonly ILogger<RegistroSubclasseRepository> logger = logger;

        public async Task<IEnumerable<RegistroSubclasseVO>> GetTodosOperacionaisPorIdRegistroClasseECadfiIdAsync(long idRegistroClasse, long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as subclasses operacionais para IdRegistroClasse: {IdRegistroClasse} e CadfiId: {cadfiId}", idRegistroClasse, cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroSubclasses
                        .Where(p => p.IdRegistroClasse == idRegistroClasse && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar subclasses operacionais para IdRegistroClasse: {IdRegistroClasse} e CadfiId: {cadfiId}", idRegistroClasse, cadfiId);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroSubclasseVO>> GetTodosOperacionaisPorRegistroClasseIdECadfiIdAsync(long registroClasseId, long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todas as subclasses operacionais para RegistroClasseId: {RegistroClasseId} e CadfiId: {cadfiId}", registroClasseId, cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroSubclasses
                        .Where(p => p.RegistroClasseId == registroClasseId && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar subclasses operacionais para RegistroClasseId: {RegistroClasseId} e CadfiId: {cadfiId}", registroClasseId, cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(RegistroSubclasse ent)
        {
            return Task.CompletedTask;
        }
    }
}