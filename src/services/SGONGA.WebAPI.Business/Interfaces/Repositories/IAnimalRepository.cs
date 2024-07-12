using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IAnimalRepository : IRepository<Animal>
{
    new Task<PagedResult<Animal>> GetAllPagedAsync(Expression<Func<Animal, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
}
