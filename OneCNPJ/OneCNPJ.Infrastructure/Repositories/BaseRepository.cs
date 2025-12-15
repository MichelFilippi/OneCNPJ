using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneCNPJ.Data;
using OneCNPJ.Domain;
using OneCNPJ.DTOs;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TEntityVO, TEntityListVO>(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger logger)
        : IBaseRepository<TEntity, TEntityVO, TEntityListVO>
        where TEntity : class, IEntity
        where TEntityListVO : class, IEntityListVO
        where TEntityVO : class, IEntityVO<TEntity, TEntityVO, TEntityListVO>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        protected readonly ILogger _logger = logger;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
                _logger.LogInformation("Entidade adicionada com sucesso: {EntityType}", typeof(TEntity).Name);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar entidade do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task BulkInsertAsync(IEnumerable<TEntityVO> entitiesVO)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var entities = entitiesVO.Select(detail => detail.ToDomain()).ToList();
                await context.Set<TEntity>().AddRangeAsync(entities);
                await context.SaveChangesAsync();
                _logger.LogInformation("Bulk insert realizado para {Count} entidades do tipo {EntityType}.", entities.Count, typeof(TEntity).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir registros em massa do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task BulkUpdateAsync(IEnumerable<TEntityVO> entities)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var entitiesVO = entities.Select(detail => detail.ToDomain()).ToList();
                context.Set<TEntity>().UpdateRange(entitiesVO);
                await context.SaveChangesAsync();
                _logger.LogInformation("Bulk update realizado para {Count} entidades do tipo {EntityType}.", entitiesVO.Count, typeof(TEntity).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar registros em massa do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntityVO> Create(TEntityVO entity)
        {
            try
            {
                var createdEntity = await AddAsync(entity.ToDomain());
                var vo = Activator.CreateInstance<TEntityVO>();
                _logger.LogInformation("Entidade criada com sucesso: {EntityType}", typeof(TEntity).Name);
                return vo.FromDomain(createdEntity);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Erro de validação ao criar entidade do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar entidade do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> Delete(long entityId)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(entityId);
                if (entity == null)
                {
                    _logger.LogWarning("Entidade do tipo {EntityType} com id {EntityId} não encontrada para exclusão.", typeof(TEntity).Name, entityId);
                    return false;
                }

                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
                _logger.LogInformation("Entidade do tipo {EntityType} com id {EntityId} removida com sucesso.", typeof(TEntity).Name, entityId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover entidade do tipo {EntityType} com id {EntityId}.", typeof(TEntity).Name, entityId);
                throw;
            }
        }

        public async Task<TEntityVO?> GetRegistroPorIdAsync(long entityId)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var entity = await context.Set<TEntity>().FindAsync(entityId);
                if (entity == null)
                {
                    _logger.LogWarning("Entidade do tipo {EntityType} com id {EntityId} não encontrada.", typeof(TEntity).Name, entityId);
                    return null;
                }

                var vo = Activator.CreateInstance<TEntityVO>();
                _logger.LogInformation("Entidade do tipo {EntityType} com id {EntityId} recuperada com sucesso.", typeof(TEntity).Name, entityId);
                return vo.FromDomain(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar entidade do tipo {EntityType} com id {EntityId}.", typeof(TEntity).Name, entityId);
                throw;
            }
        }

        public async Task<TEntityVO?> GetDetailAsync(Func<ApplicationDbContext, Task<TEntity?>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var entity = await query(context);
                if (entity == null)
                {
                    _logger.LogWarning("Nenhum detalhe encontrado para entidade do tipo {EntityType}.", typeof(TEntity).Name);
                    return null!;
                }

                var vo = Activator.CreateInstance<TEntityVO>();
                _logger.LogInformation("Detalhe recuperado para entidade do tipo {EntityType}.", typeof(TEntity).Name);
                return vo.FromDomain(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar detalhe para entidade do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<IEnumerable<TEntityVO>> GetDetailsAsync(Func<ApplicationDbContext, IQueryable<TEntity>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var ents = await query(context).ToListAsync();
                var detalhes = await GetDetalhes(ents);
                _logger.LogInformation("Recuperados {Count} registros do tipo {EntityType}.", detalhes.Count, typeof(TEntity).Name);
                return FromDomain(detalhes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar lista de entidades do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<IEnumerable<TEntityListVO>> GetListDetailsAsync(Func<ApplicationDbContext, IQueryable<TEntity>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var ents = await query(context).ToListAsync();
                var detalhes = await GetDetalhes(ents);
                _logger.LogInformation("Recuperados {Count} registros do tipo {EntityType}.", detalhes.Count, typeof(TEntity).Name);
                return FromDomainList(detalhes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar lista de entidades do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntityVO> Update(TEntityVO entity)
        {
            try
            {
                var updatedEntity = await UpdateAsync(entity.ToDomain());
                var vo = Activator.CreateInstance<TEntityVO>();
                _logger.LogInformation("Entidade do tipo {EntityType} atualizada com sucesso.", typeof(TEntity).Name);
                return vo.FromDomain(updatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar entidade do tipo {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                context.Set<TEntity>().Update(entity);
                await context.SaveChangesAsync();
                _logger.LogInformation("Entidade do tipo {EntityType} atualizada com sucesso (UpdateAsync).", typeof(TEntity).Name);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar entidade do tipo {EntityType} (UpdateAsync).", typeof(TEntity).Name);
                throw;
            }
        }

        private static List<TEntityVO> FromDomain(List<TEntity> lista)
        {
            List<TEntityVO> toReturn = [];
            foreach (TEntity obj in lista)
            {
                var vo = Activator.CreateInstance<TEntityVO>();
                toReturn.Add(vo.FromDomain(obj));
            }
            return toReturn;
        }

        private static List<TEntityListVO> FromDomainList(List<TEntity> lista)
        {
            List<TEntityListVO> toReturn = [];
            foreach (TEntity obj in lista)
            {
                var vo = Activator.CreateInstance<TEntityVO>();
                toReturn.Add(vo.ListFromDomain(obj));
            }
            return toReturn;
        }

        private async Task<List<TEntity>> GetDetalhes(List<TEntity> lista)
        {
            foreach (var ent in lista)
            {
                await FillInDetails(ent);
            }
            return lista;
        }

        protected abstract Task FillInDetails(TEntity ent);
    }
}