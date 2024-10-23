using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IRepository<T> : IDisposable where T : Entity
{
    Task<Result> AddAsync(T entity);
    Result Update(T entity);
    Result Delete(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<T>> GetByIdWithoutTenantAsync(Guid id);
    Task<Result<List<T>>> GetAllAsync();
    Task<Result<PagedResult<T>>> GetAllPagedAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<Result<PagedResult<T>>> GetAllPagedWithoutTenantAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false);
    Task<Result<IEnumerable<T>>> SearchAsync(Expression<Func<T, bool>> predicate);
    Task<Result<IEnumerable<T>>> SearchWithoutTenantAsync(Expression<Func<T, bool>> predicate);
}
