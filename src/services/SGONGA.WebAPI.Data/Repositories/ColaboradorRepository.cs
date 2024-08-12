using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class ColaboradorRepository : Repository<Colaborador>, IColaboradorRepository
{
    public ColaboradorRepository(ONGDbContext db) : base(db)
    {
    }
}