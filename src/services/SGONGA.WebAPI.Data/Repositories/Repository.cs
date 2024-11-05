using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Entities;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ONGDbContext Db;
    protected readonly DbSet<T> DbSet;

    protected Repository(ONGDbContext db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }
    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
                          .Where(predicate)
                          .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public virtual async Task<Result<T>> GetByIdAsync(Guid id)
    {
        return await DbSet.AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}