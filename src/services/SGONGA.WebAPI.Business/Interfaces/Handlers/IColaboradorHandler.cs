using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IColaboradorHandler
{
    Task CreateAsync(CreateColaboradorRequest request);
    Task UpdateAsync(UpdateColaboradorRequest request);
    Task DeleteAsync(DeleteColaboradorRequest request);
    Task<ColaboradorResponse> GetByIdAsync(GetColaboradorByIdRequest request);
    Task<PagedResult<ColaboradorResponse>> GetAllAsync(GetAllColaboradoresRequest request);
}
