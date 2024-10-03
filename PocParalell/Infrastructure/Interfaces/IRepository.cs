using PocParalell.Domain;
using System.Linq.Expressions;

namespace PocParalell.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseModel
    {
        Task AdicionarAsync(TEntity entity);
        Task AdicionarLoteAsync(List<TEntity> entity);
        Task<TEntity> ObterPorIdAsync(Guid id);

        Task<List<TEntity>> ObterTodosAsync();

        Task<IEnumerable<TEntity>> ObterTodosQueryableAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

        Task<TEntity> ObterQueryableAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

        Task<IEnumerable<TEntity>> ObterTodosAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> BuscarListaAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> BuscarListaAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> BuscarAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task AtualizarAsync(TEntity entity, byte[] rowVersion);

        Task AtualizarAsync(TEntity entity);

        Task RemoverAsync(Guid id);

        Task<int> SaveChangesAsync();
    }
}