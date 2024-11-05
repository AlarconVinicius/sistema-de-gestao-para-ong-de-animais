using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Responses;

namespace SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;

public interface IAnimalRepository : IRepository<Animal>
{
    Task<AnimalResponse> GetByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<AnimalResponse>> GetAllAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
}
