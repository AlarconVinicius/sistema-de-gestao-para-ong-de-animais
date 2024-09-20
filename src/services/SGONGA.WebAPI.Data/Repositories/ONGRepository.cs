using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class ONGRepository : Repository<ONG>, IONGRepository
{
    public ONGRepository(ONGDbContext db) : base(db)
    {
    }
    public async Task<Result<ONG>> GetByIdWithAnimalsAsync(Guid id)
    {
        return await DbSet.AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }
    public async Task<Result<ONG>> GetByIdWithAnimalsWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }
    public async Task<Result<ONG>> GetBySlugAsync(string slug)
    {
        return await DbSet.AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Slug == slug) ?? null!;
    }
    public async Task<Result<ONG>> GetBySlugWithoutTenantAsync(string slug)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Slug == slug) ?? null!;
    }
}
