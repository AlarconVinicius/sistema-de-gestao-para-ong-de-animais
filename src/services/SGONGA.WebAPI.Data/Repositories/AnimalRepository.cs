using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;
using System.Linq;

namespace SGONGA.WebAPI.Data.Repositories;

public class AnimalRepository : Repository<Animal>, IAnimalRepository
{
    public AnimalRepository(ONGDbContext db) : base(db)
    {
    }

    public override async Task<Result<Animal>> GetByIdAsync(Guid id)
    {
        return await DbSet.Include(q => q.ONG)
                          .AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public override async Task<Result<Animal>> GetByIdWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .Include(q => q.ONG)
                          .AsNoTracking()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public override async Task<Result<PagedResult<Animal>>> GetAllPagedAsync(Expression<Func<Animal, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<Animal>();

        var queryable = DbSet.Include(q => q.ONG).AsNoTracking().AsQueryable();

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        result.TotalResults = await queryable.CountAsync();

        if (!returnAll)
        {
            queryable = queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        return new PagedResult<Animal>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }
    public override async Task<Result<PagedResult<Animal>>> GetAllPagedWithoutTenantAsync(Expression<Func<Animal, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<Animal>();

        var queryable = DbSet.IgnoreQueryFilters().Include(q => q.ONG).AsNoTracking().AsQueryable();

        if (predicate != null)
        {
            queryable = queryable.Where(predicate);
        }

        result.TotalResults = await queryable.CountAsync();

        if (!returnAll)
        {
            queryable = queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        return new PagedResult<Animal>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }
}
