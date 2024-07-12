using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IONGHandler
{
    Task CreateAsync(CreateONGRequest request);
    Task UpdateAsync(UpdateONGRequest request);
    Task DeleteAsync(DeleteONGRequest request);
    Task<ONGResponse> GetByIdAsync(GetONGByIdRequest request);
    Task<PagedResult<ONGResponse>> GetAllAsync(GetAllONGsRequest request);
}
