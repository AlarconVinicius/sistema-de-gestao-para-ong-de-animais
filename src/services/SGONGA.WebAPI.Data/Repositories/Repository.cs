using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity, new()
{
    protected readonly ONGDbContext Db;
    protected readonly DbSet<T> DbSet;

    protected Repository(ONGDbContext db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }
    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.AnyAsync(predicate);
    }

    public virtual async Task<Result<IEnumerable<T>>> SearchAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.AsNoTracking()
                          .Where(predicate)
                          .ToListAsync();
    }

    public virtual async Task<Result<IEnumerable<T>>> SearchWithoutTenantAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .Where(predicate)
                          .ToListAsync();
    }

    public virtual async Task<Result<T>> GetByIdAsync(Guid id)
    {
        return await DbSet.AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public virtual async Task<Result<T>> GetByIdWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public virtual async Task<Result<List<T>>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<Result<PagedResult<T>>> GetAllPagedAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<T>();

        var queryable = DbSet.AsQueryable();

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        result.TotalResults = await queryable.CountAsync();

        if (!returnAll)
        {
            queryable = queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        return new PagedResult<T>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }

    public virtual async Task<Result<PagedResult<T>>> GetAllPagedWithoutTenantAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<T>();

        var queryable = DbSet.IgnoreQueryFilters().AsQueryable();

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        result.TotalResults = await queryable.CountAsync();

        if (!returnAll)
        {
            queryable = queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        return new PagedResult<T>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }

    public virtual async Task<Result> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return Result.Ok();
    }

    public virtual Result Update(T entity)
    {
        DbSet.Update(entity);
        return Result.Ok();
    }

    public virtual Result Delete(Guid id)
    {
        DbSet.Remove(new T { Id = id });
        return Result.Ok();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}