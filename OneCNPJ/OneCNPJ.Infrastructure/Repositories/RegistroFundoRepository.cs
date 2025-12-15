using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Utilities;
using OneCNPJ.Data;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repositories.Interfaces;

namespace OneCNPJ.Infrastructure.Repositories
{
    public class RegistroFundoRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<RegistroFundoRepository> logger)
        : BaseRepository<RegistroFundo, RegistroFundoVO, RegistroFundoListaVO>(contextFactory, logger),
        IRegistroFundoRepository
    {
        private readonly ILogger<RegistroFundoRepository> logger = logger;

        public async Task<IEnumerable<RegistroFundoVO>> GetTodosOperacionaisPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todos os fundos operacionais para CadfiId: {CadfiId}", cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroFundos
                        .Where(p => p.CadfiId == cadfiId && (int)p.Status >= 500)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar fundos operacionais para CadfiId: {CadfiId}", cadfiId);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroFundoVO>> GetTodosPorCadfiIdAsync(long cadfiId)
        {
            try
            {
                logger.LogInformation("Buscando todos os fundos para CadfiId: {CadfiId}", cadfiId);
                return await GetDetailsAsync(
                    context => context.RegistroFundos
                        .Where(p => p.CadfiId == cadfiId)
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar fundos para CadfiId: {CadfiId}", cadfiId);
                throw;
            }
        }

        public async Task<RegistroFundoVO?> GetRegistroPorCnpjFundoAsync(string cnpjFundo)
        {
            try
            {
                var cnpjFormatado = General.CnpjFormatado(cnpjFundo, true);
                logger.LogInformation("Buscando fundo por CNPJ: {Cnpj}", cnpjFormatado);
                var result = await GetDetailAsync(
                    context => context.RegistroFundos
                        .Where(p => p.CnpjFundo == cnpjFormatado && (int)p.Status >= 500)
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum fundo encontrado para CNPJ: {Cnpj}", cnpjFormatado);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar fundo por CNPJ: {Cnpj}", cnpjFundo);
                throw;
            }
        }

        public async Task<RegistroFundoVO?> GetRegistroPorCnpjFundoECadfiIdAsync(string cnpjFundo, long cadfiId)
        {
            try
            {
                var cnpjFormatado = General.CnpjFormatado(cnpjFundo, true);
                logger.LogInformation("Buscando fundo por CNPJ: {Cnpj} e CadfiId: {CadfiId}", cnpjFormatado, cadfiId);
                var result = await GetDetailAsync(
                    context => context.RegistroFundos
                        .Where(p => p.CnpjFundo == cnpjFormatado && (int)p.Status >= 500 && p.CadfiId == cadfiId)
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum fundo encontrado para CNPJ: {Cnpj} e CadfiId: {CadfiId}", cnpjFormatado, cadfiId);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar fundo por CNPJ: {Cnpj} e CadfiId: {CadfiId}", cnpjFundo, cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(RegistroFundo ent)
        {
            return Task.CompletedTask;
        }
    }
}