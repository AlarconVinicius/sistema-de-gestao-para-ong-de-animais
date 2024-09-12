using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IAdotanteRepository : IRepository<Adotante>
{
    Task<Adotante> GetByIdWithoutTenantAsync(Guid id);
    Task<PagedResult<Adotante>> GetAllPagedWithoutTenantAsync(Expression<Func<Adotante, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<IEnumerable<Adotante>> SearchWithoutTenantAsync(Expression<Func<Adotante, bool>> predicate);
}
