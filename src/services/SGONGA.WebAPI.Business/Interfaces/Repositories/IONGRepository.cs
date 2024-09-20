using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IONGRepository : IRepository<ONG>
{
    Task<Result<ONG>> GetByIdWithAnimalsAsync(Guid id);

    Task<Result<ONG>> GetByIdWithAnimalsWithoutTenantAsync(Guid id);
    
    Task<Result<ONG>> GetBySlugAsync(string slug);

    Task<Result<ONG>> GetBySlugWithoutTenantAsync(string slug);
}
