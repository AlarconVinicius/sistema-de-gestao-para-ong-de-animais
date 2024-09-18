using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IONGRepository : IRepository<ONG>
{
    Task<ONG> GetByIdWithoutTenantAsync(Guid id);
    Task<PagedResult<ONG>> GetAllPagedWithoutTenantAsync(Expression<Func<ONG, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<IEnumerable<ONG>> SearchWithoutTenantAsync(Expression<Func<ONG, bool>> predicate);
}
