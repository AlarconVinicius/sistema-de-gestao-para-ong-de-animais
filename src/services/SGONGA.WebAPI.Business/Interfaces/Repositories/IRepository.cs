using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IRepository<T> : IDisposable where T : Entity
{
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<PagedResult<T>> GetAllPagedAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
}
