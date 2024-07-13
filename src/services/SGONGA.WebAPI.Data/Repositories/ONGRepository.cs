using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class ONGRepository : Repository<ONG>, IONGRepository
{
    public ONGRepository(ONGDbContext db) : base(db)
    {
    }
}
