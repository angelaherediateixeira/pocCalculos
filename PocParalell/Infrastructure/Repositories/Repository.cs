using Microsoft.EntityFrameworkCore;
using PocParalell.Domain;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PocParalell.Infrastructure.Repositories
{
    public abstract class Repository<TEntity>(SqlEntityFrameworkDbContext db) : IRepository<TEntity> where TEntity : BaseModel, new()
    {
        protected readonly SqlEntityFrameworkDbContext Db = db;
        protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> BuscarListaAsync(Expression<Func<TEntity, bool>> predicate)
         => await DbSet.AsNoTracking().Where(predicate).ToListAsync();

        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
            => await DbSet.FindAsync(id);

        public virtual async Task<List<TEntity>> ObterTodosAsync()
            => await DbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> ObterTodosAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> BuscarListaAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.AsNoTracking().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> BuscarAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.AsNoTracking().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task AdicionarAsync(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public virtual async Task AdicionarLoteAsync(List<TEntity> entity)
        {
            try
            {
                await Db.BulkInsertAsync(entity);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public virtual async Task AtualizarAsync(TEntity entity, byte[] rowVersion)
        {
            try
            {
                DbSet.Entry(entity).Property("Versao").OriginalValue = rowVersion;
                DbSet.Update(entity);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return;
            }

        }

        public virtual async Task AtualizarAsync(TEntity entity)
        {
            try
            {
                var existingEntity = await DbSet.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    return;
                }

                Db.Entry(existingEntity).CurrentValues.SetValues(entity);

                await SaveChangesAsync();

                return;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = await entry.GetDatabaseValuesAsync();
                if (databaseValues == null)
                {
                    return;
                }

                var databaseEntry = databaseValues.ToObject();
                entry.OriginalValues.SetValues(databaseEntry);

                await SaveChangesAsync();
                return;
            }
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            try
            {
                var entity = await DbSet.FindAsync(id);
                if (entity != null)
                {
                    DbSet.Remove(entity);
                    await SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async Task<int> SaveChangesAsync()
            => await Db.SaveChangesAsync();

        public void Dispose()
        {
            Db?.Dispose();
        }

        public async Task<TEntity> ObterQueryableAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            try
            {
                var query = DbSet.AsNoTracking();

                if (include != null)
                {
                    query = include(query);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public async Task<IEnumerable<TEntity>> ObterTodosQueryableAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }
    }
}