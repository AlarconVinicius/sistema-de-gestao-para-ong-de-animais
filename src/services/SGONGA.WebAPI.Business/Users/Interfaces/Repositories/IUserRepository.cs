using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Users.Responses;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Users.Interfaces.Repositories;
public interface IUserRepository : IRepository<Usuario>
{
    Task<Usuario> SearchAsync(Expression<Func<Usuario, bool>> predicate, CancellationToken cancellationToken = default);
    Task<ONG> GetNGOByIdWithAnimalsAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);

    Task<Result<ONG>> GetBySlugAsync(string slug, Guid? tenantId, CancellationToken cancellationToken = default);

    Task<Result<EUsuarioTipo>> IdentifyUserType(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<UserResponse> GetAdopterByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<UserResponse> GetNGOByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<UserResponse>> GetAllAdoptersPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<UserResponse>> GetAllNGOsPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
}
