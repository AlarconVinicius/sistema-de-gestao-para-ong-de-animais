using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IColaboradorRepository : IRepository<Colaborador>
{
    Task<Colaborador> GetByIdAsync(Guid id, Guid tenantId);
}
