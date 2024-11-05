using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Entities;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;

public interface IRepository<T> : IDisposable where T : Entity
{
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<T> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
