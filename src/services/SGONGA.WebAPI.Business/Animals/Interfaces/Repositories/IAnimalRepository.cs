using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Shared.Responses;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;

public interface IAnimalRepository : IRepository<Animal>
{
    Task<Animal> SearchAsync(Expression<Func<Animal, bool>> predicate, CancellationToken cancellationToken = default);
    Task<AnimalResponse> GetByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<AnimalResponse>> GetAllAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
}
