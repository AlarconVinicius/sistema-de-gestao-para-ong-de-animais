using SGONGA.WebAPI.Business.Shared.Entities;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
public interface IGenericUnitOfWork
{
    Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : Entity;

    Task InsertRangeAsync<TEntity>(IList<TEntity> entities, CancellationToken cancellationToken) where TEntity : Entity;

    void Update<TEntity>(TEntity entity) where TEntity : Entity;

    void Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : Entity;

    void Update<TEntity>(TEntity entity, params string[] properties) where TEntity : Entity;

    void UpdateRange<TEntity>(IList<TEntity> entities) where TEntity : Entity;

    void Delete<TEntity>(TEntity entity) where TEntity : Entity;

    void DeleteRange<TEntity>(IList<TEntity> entities) where TEntity : Entity;

    Task<TEntity> GetByPrimaryKeyForEditAsync<TEntity>(params object[] keys) where TEntity : Entity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
