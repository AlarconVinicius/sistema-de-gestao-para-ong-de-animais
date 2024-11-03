using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class ONGRepository : Repository<NGO>, IONGRepository
{
    public ONGRepository(ONGDbContext db) : base(db)
    {
    }
    public async Task<Result<NGO>> GetByIdWithAnimalsAsync(Guid id)
    {
        return await DbSet.AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }
    public async Task<Result<NGO>> GetByIdWithAnimalsWithoutTenantAsync(Guid id)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }
    public async Task<Result<NGO>> GetBySlugAsync(string slug)
    {
        return await DbSet.AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Slug == slug) ?? null!;
    }
    public async Task<Result<NGO>> GetBySlugWithoutTenantAsync(string slug)
    {
        return await DbSet.IgnoreQueryFilters()
                          .AsNoTracking()
                          .Include(c => c.Animais)
                          .FirstOrDefaultAsync(c => c.Slug == slug) ?? null!;
    }
}
