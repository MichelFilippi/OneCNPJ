using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.Infrastructure.Repository.Interfaces;

namespace OneCNPJ.Infrastructure.Repository
{
    public class CnpjImportacaoRepository(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<CnpjImportacaoRepository> logger)
        : BaseRepository<CnpjImportacao, CnpjImportacaoVO, CnpjImportacaoListaVO>(contextFactory, logger),
          ICnpjImportacaoRepository
    {
        private readonly ILogger<CnpjImportacaoRepository> logger = logger;

        private static string OnlyDigits(string s)
            => new string((s ?? "").Where(char.IsDigit).ToArray());

        private static string GetBasicoFromCnpj(string cnpj14)
        {
            var dig = OnlyDigits(cnpj14);
            return dig.Length >= 8 ? dig.Substring(0, 8) : dig;
        }
        private static IQueryable<CnpjImportacaoVO> IncludeGraph(IQueryable<CnpjImportacaoVO> q)
        {
            return q
                .Include(i => i.CnpjEmpresa) // precisa existir a navegação em CnpjImportacao
                    .ThenInclude(e => e.NaturezaJuridica)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.QualificacaoSocio)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Socios)
                        .ThenInclude(s => s.QualificacaoSocio)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Simples)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Estabelecimentos)
                        .ThenInclude(est => est.Municipio)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Estabelecimentos)
                        .ThenInclude(est => est.Pais)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Estabelecimentos)
                        .ThenInclude(est => est.MotivoSituacaoCadastral)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Estabelecimentos)
                        .ThenInclude(est => est.Cnae)
                .Include(i => i.CnpjEmpresa)
                    .ThenInclude(e => e.Estabelecimentos)
                        .ThenInclude(est => est.CnaesSecundarios)
                            .ThenInclude(sec => sec.Cnae);
        }

        public async Task<CnpjImportacaoVO?> GetAtualAsync()
        {
            // “Atual” = última importação OK (ou Ajuste: Status >= 500)
            return await GetDetailAsync(async context =>
            {
                var query = context.Set<CnpjImportacao>()
                    .AsNoTracking()
                    .Where(x => (int)x.Status >= 500)
                    .OrderBy(x => x.Id);

                var ent = await query.LastOrDefaultAsync();
                return ent;
            });
        }

        public async Task<CnpjImportacaoVO?> GetUltimoAsync()
        {
            return await GetDetailAsync(async context =>
            {
                var ent = await context.Set<CnpjImportacao>()
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync();

                return ent;
            });
        }

        public async Task<IEnumerable<CnpjImportacaoListaVO>> GetUltimosAsync(int rows)
        {
            return await GetListDetailsAsync(context =>
                context.Set<CnpjImportacao>()
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .Take(rows)
            );
        }

        public async Task<IEnumerable<CnpjImportacaoListaVO>> GetTodosProcessandoAsync()
        {
            return await GetListDetailsAsync(context =>
                context.Set<CnpjImportacao>()
                    .AsNoTracking()
                    .Where(x => x.Status == StatusEnum.Processamento)
                    .OrderByDescending(x => x.Id)
            );
        }

        public async Task<IEnumerable<CnpjImportacaoListaVO>> GetTodosOkAsync()
        {
            return await GetListDetailsAsync(context =>
                context.Set<CnpjImportacao>()
                    .AsNoTracking()
                    .Where(x => (int)x.Status >= 500)
                    .OrderByDescending(x => x.Id)
            );
        }

        public async Task<CnpjImportacaoVO?> GetRegistroPorCnpjAsync(string cnpj14, string traceId)
        {
            var basico = GetBasicoFromCnpj(cnpj14);

            logger.LogInformation(
                "TraceId {TraceId} - Buscando importação (raiz) por CNPJ {Cnpj} (basico={Basico})",
                traceId, cnpj14, basico);

            using var context = contextFactory.CreateDbContext();

            var baseQuery = context.Set<CnpjImportacaoVO>()
                .AsNoTracking()
                .OrderByDescending(x => x.Id);

            var query = IncludeGraph(baseQuery);

            var ent = await query
                .Where(i => i.CnpjEmpresa.Any(e => e.CnpjBasico == basico))
                .FirstOrDefaultAsync();

            return ent;
        }

        protected override Task FillInDetails(CnpjImportacao ent)
            => Task.CompletedTask;
    }
}
