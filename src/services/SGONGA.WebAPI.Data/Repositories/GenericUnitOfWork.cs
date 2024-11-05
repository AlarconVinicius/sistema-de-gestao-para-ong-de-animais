using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Shared.Entities;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

internal sealed class GenericUnitOfWork : IGenericUnitOfWork
{
    private readonly ONGDbContext _context;

    public GenericUnitOfWork(ONGDbContext context)
    {
        _context = context;
    }

    public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : Entity
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public async Task InsertRangeAsync<TEntity>(IList<TEntity> entities, CancellationToken cancellationToken) where TEntity : Entity
    {
        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : Entity
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Attach(entity);
        }
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : Entity
    {
        foreach (var expression in properties)
        {
            string propertyName = expression.Body is MemberExpression memberExpression
                ? memberExpression.Member.Name
                : ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;

            _context.Entry(entity).Property(propertyName).IsModified = true;
        }
    }

    public void Update<TEntity>(TEntity entity, params string[] properties) where TEntity : Entity
    {
        foreach (var propertyName in properties)
        {
            _context.Entry(entity).Property(propertyName).IsModified = true;
        }
    }

    public void UpdateRange<TEntity>(IList<TEntity> entities) where TEntity : Entity
    {
        _context.UpdateRange(entities);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : Entity
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Attach(entity);
        }
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void DeleteRange<TEntity>(IList<TEntity> entities) where TEntity : Entity
    {
        _context.RemoveRange(entities);
    }

    public async Task<TEntity> GetByPrimaryKeyForEditAsync<TEntity>(params object[] keys) where TEntity : Entity
    {
        return await _context.FindAsync<TEntity>(keys) ?? null!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}