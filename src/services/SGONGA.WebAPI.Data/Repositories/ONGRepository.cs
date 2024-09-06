using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

public class ONGRepository : Repository<ONG>, IONGRepository
{
    public ONGRepository(ONGDbContext db) : base(db)
    {
    }

    public async Task<ONG> GetByIdWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public async Task<PagedResult<ONG>> GetAllPagedWithoutTenantAsync(Expression<Func<ONG, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<ONG>();

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

        return new PagedResult<ONG>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }

    public override async Task<IEnumerable<ONG>> SearchAsync(Expression<Func<ONG, bool>> predicate)
    {
        return await DbSet.AsNoTracking().IgnoreQueryFilters().Where(predicate).ToListAsync();
    }
}
