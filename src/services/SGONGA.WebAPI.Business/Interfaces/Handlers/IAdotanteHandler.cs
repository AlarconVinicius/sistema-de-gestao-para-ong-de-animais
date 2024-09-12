using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IAdotanteHandler
{
    Task CreateAsync(CreateAdotanteRequest request);
    Task UpdateAsync(UpdateAdotanteRequest request);
    Task DeleteAsync(DeleteAdotanteRequest request);
    Task<AdotanteResponse> GetByIdAsync(GetAdotanteByIdRequest request);
    Task<PagedResponse<AdotanteResponse>> GetAllAsync(GetAllAdotantesRequest request);
}
