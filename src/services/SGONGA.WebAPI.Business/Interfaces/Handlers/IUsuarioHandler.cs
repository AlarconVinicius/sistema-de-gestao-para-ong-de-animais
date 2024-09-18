using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IUsuarioHandler
{
    Task CreateAsync(CreateUsuarioRequest request);
    Task DeleteAsync(DeleteUsuarioRequest request);
    Task<PagedResponse<UsuarioResponse>> GetAllAsync(GetAllUsuariosRequest request);
    Task<UsuarioResponse> GetByIdAsync(GetUsuarioByIdRequest request);
    Task UpdateAsync(UpdateUsuarioRequest request);
}