using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class AnimalRepository : Repository<Animal>, IAnimalRepository
{
    public AnimalRepository(ONGDbContext db) : base(db)
    {
    }

    public async Task<Animal> GetByIdAsync(Guid id, Guid tenantId)
    {
        if (tenantId == Guid.Empty) return null!;

        return await DbSet.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId) ?? null!;
    }
}
