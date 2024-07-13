using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class ColaboradorRepository : Repository<Colaborador>, IColaboradorRepository
{
    public ColaboradorRepository(ONGDbContext db) : base(db)
    {
    }

    public async Task<Colaborador> GetByIdAsync(Guid id, Guid tenantId)
    {
        if (tenantId == Guid.Empty) return null!;

        return await DbSet.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId) ?? null!;
    }
}