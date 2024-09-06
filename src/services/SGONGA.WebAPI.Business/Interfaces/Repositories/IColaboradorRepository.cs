using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IColaboradorRepository : IRepository<Colaborador>
{
    Task<Colaborador> GetByIdWithoutTenantAsync(Guid id);
    Task<PagedResult<Colaborador>> GetAllPagedWithoutTenantAsync(Expression<Func<Colaborador, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<IEnumerable<Colaborador>> SearchWithoutTenantAsync(Expression<Func<Colaborador, bool>> predicate);
}
