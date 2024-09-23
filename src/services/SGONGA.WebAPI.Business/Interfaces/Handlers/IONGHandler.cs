using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IONGHandler
{
    Task<Result> CreateAsync(CreateUsuarioRequest request);
    Task<Result> UpdateAsync(UpdateUsuarioRequest request);
    Task<Result> DeleteAsync(DeleteUsuarioRequest request);
    Task<Result<UsuarioResponse>> GetByIdAsync(GetUsuarioByIdRequest request);
    Task<Result<PagedResponse<UsuarioResponse>>> GetAllAsync(GetAllUsuariosRequest request);
}
