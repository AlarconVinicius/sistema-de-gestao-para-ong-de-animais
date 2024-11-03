using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IONGRepository : IRepository<NGO>
{
    Task<Result<NGO>> GetByIdWithAnimalsAsync(Guid id);

    Task<Result<NGO>> GetByIdWithAnimalsWithoutTenantAsync(Guid id);
    
    Task<Result<NGO>> GetBySlugAsync(string slug);

    Task<Result<NGO>> GetBySlugWithoutTenantAsync(string slug);
}
