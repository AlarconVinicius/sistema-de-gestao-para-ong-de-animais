using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;
using System.Linq;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

public class ColaboradorRepository : Repository<Colaborador>, IColaboradorRepository
{
    public ColaboradorRepository(ONGDbContext db) : base(db)
    {
    }

    public async Task<Colaborador> GetByIdWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public async Task<PagedResult<Colaborador>> GetAllPagedWithoutTenantAsync(Expression<Func<Colaborador, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false)
    {
        var result = new PagedResult<Colaborador>();

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

        return new PagedResult<Colaborador>()
        {
            List = await queryable.ToListAsync(),
            TotalResults = await queryable.CountAsync(),
            PageIndex = page,
            PageSize = pageSize,
            Query = query
        };
    }

    public async Task<IEnumerable<Colaborador>> SearchWithoutTenantAsync(Expression<Func<Colaborador, bool>> predicate)
    {
        return await DbSet.AsNoTracking().IgnoreQueryFilters().Where(predicate).ToListAsync();
    }
}