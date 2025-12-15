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
    public class ConteudoRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<ConteudoRepository> logger)
        : BaseRepository<Conteudo, ConteudoVO, ConteudoListaVO>(contextFactory, logger),
        IConteudoRepository
    {
        private readonly ILogger<ConteudoRepository> logger = logger;

        public async Task<ConteudoVO?> GetRegistroPorCnpjFundoAsync(string cnpjFundo)
        {
            try
            {
                var cnpjFormatado = General.CnpjFormatado(cnpjFundo, true);
                logger.LogInformation("Buscando registro Conteúdo por CNPJ do fundo: {Cnpj}", cnpjFormatado);
                var result = await GetDetailAsync(
                    context => context.Conteudos
                        .Where(p => p.Cnpj == cnpjFormatado && (int)p.Status >= 500)
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum registro Conteúdo encontrado para CNPJ: {Cnpj}", cnpjFormatado);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro Conteúdo por CNPJ do fundo: {Cnpj}", cnpjFundo);
                throw;
            }
        }

        public async Task<ConteudoVO?> GetRegistroPorCnpjFundoECadfiIdAsync(string cnpjFundo, long cadfiId)
        {
            try
            {
                var cnpjFormatado = General.CnpjFormatado(cnpjFundo, true);
                logger.LogInformation("Buscando registro Conteúdo por CNPJ do fundo: {Cnpj} e CadfiId: {CadfiId}", cnpjFormatado, cadfiId);
                var result = await GetDetailAsync(
                    context => context.Conteudos
                        .Where(p => p.Cnpj == cnpjFormatado && (int)p.Status >= 500 && p.CadfiId == cadfiId)
                        .OrderBy(p => p.Id)
                        .LastOrDefaultAsync()
                );
                if (result == null)
                    logger.LogWarning("Nenhum registro Conteúdo encontrado para CNPJ: {Cnpj} e CadfiId: {CadfiId}", cnpjFormatado, cadfiId);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar registro Conteúdo por CNPJ do fundo: {Cnpj} e CadfiId: {CadfiId}", cnpjFundo, cadfiId);
                throw;
            }
        }

        protected override Task FillInDetails(Conteudo ent)
        {
            return Task.CompletedTask;
        }
    }
}