using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class AdotanteRepository : Repository<Adopter>, IAdotanteRepository
{
    public AdotanteRepository(ONGDbContext db) : base(db)
    {
    }
}