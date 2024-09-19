using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IAdotanteHandler
{
    Task CreateAsync(CreateUsuarioRequest request);
    Task UpdateAsync(UpdateUsuarioRequest request);
    Task DeleteAsync(DeleteUsuarioRequest request);
    Task<UsuarioResponse> GetByIdAsync(GetUsuarioByIdRequest request);
    Task<PagedResponse<UsuarioResponse>> GetAllAsync(GetAllUsuariosRequest request);
}
