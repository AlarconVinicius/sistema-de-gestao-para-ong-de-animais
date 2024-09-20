using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class AnimalRepository : Repository<Animal>, IAnimalRepository
{
    public AnimalRepository(ONGDbContext db) : base(db)
    {
    }
}
