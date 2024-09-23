using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class UsuarioHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, IAdotanteHandler adotanteHandler, IONGHandler ongHandler, IIdentityHandler identityHandler) : BaseHandler(notifier, appUser), IUsuarioHandler
{
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IAdotanteHandler _adotanteHandler = adotanteHandler;
    public readonly IONGHandler _ongHandler = ongHandler;
    public readonly IIdentityHandler _identityHandler = identityHandler;

    public async Task<Result<UsuarioResponse>> GetByIdAsync(GetUsuarioByIdRequest request)
    {
        var usuarioTipo = IdentificarTipoUsuario(request.Id, request.TenantFiltro);
        if (usuarioTipo.IsFailed) 
            return UsuarioErrors.UsuarioNaoEncontrado(request.Id);

        Result<UsuarioResponse> response;
        switch (usuarioTipo.Value)
        {
            case EUsuarioTipo.Adotante:
                response = await _adotanteHandler.GetByIdAsync(request);
                break;

            case EUsuarioTipo.ONG:
                response = await _ongHandler.GetByIdAsync(request);
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelRecuperarUsuario;
        }

        return response.IsSuccess ? response.Value : response.Errors;
    }

    public async Task<Result<PagedResponse<UsuarioResponse>>> GetAllAsync(GetAllUsuariosRequest request)
    {
        Result<PagedResponse<UsuarioResponse>> response;
        switch (request.UsuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                response = await _adotanteHandler.GetAllAsync(request);
                break;

            case EUsuarioTipo.ONG:
                response = await _ongHandler.GetAllAsync(request);
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelRecuperarUsuarios;
        }

        return response.IsSuccess ? response.Value : response.Errors;
    }

    public async Task<Result> CreateAsync(CreateUsuarioRequest request)
    {
        Result userResult;
        Result identityResult;
        switch (request.UsuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                userResult = await _adotanteHandler.CreateAsync(request);
                if (userResult.IsFailed)
                    return userResult.Errors;

                identityResult = await _identityHandler.CreateAsync(new CreateUserRequest(request.Id, request.Contato.Email, request.Senha, request.ConfirmarSenha));
                if (identityResult.IsFailed)
                    return identityResult.Errors;

                await _identityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Id, new UserClaim("Tenant", request.TenantId.ToString())));
                break;

            case EUsuarioTipo.ONG:
                userResult = await _ongHandler.CreateAsync(request);
                if (userResult.IsFailed)
                    return userResult.Errors;

                identityResult = await _identityHandler.CreateAsync(new CreateUserRequest(request.Id, request.Contato.Email, request.Senha, request.ConfirmarSenha));
                if (identityResult.IsFailed)
                    return identityResult.Errors;

                await _identityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Id, new UserClaim("Tenant", request.TenantId.ToString())));
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelCriarUsuario;
        }

        Result commitResult = await _unitOfWork.CommitAsync();

        return commitResult.IsSuccess ? Result.Ok() : commitResult.Errors;
    }

    public async Task<Result> UpdateAsync(UpdateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new AdotanteValidation(), adotante)) return;

        Result result;
        switch (request.UsuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                result = await _adotanteHandler.UpdateAsync(request);
                break;

            case EUsuarioTipo.ONG:
                result = await _ongHandler.UpdateAsync(request);
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelAtualizarUsuario;
        }

        if (result.IsFailed)
            return result.Errors;

        Result commitResult = await _unitOfWork.CommitAsync();

        return commitResult.IsSuccess ? Result.Ok() : commitResult.Errors;
    }

    public async Task<Result> DeleteAsync(DeleteUsuarioRequest request)
    {
        var usuarioTipo = IdentificarTipoUsuario(request.Id, true).Value;
        if (usuarioTipo is null)
            return UsuarioErrors.UsuarioNaoEncontrado(request.Id);

        Result userResult;
        Result identityResult;
        switch (usuarioTipo)
        {
            case EUsuarioTipo.Adotante:

                userResult = _adotanteHandler.Delete(request);
                if(userResult.IsFailed)
                    return userResult.Errors;

                identityResult = await _identityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
                if (identityResult.IsFailed)
                    return identityResult.Errors;
                break;

            case EUsuarioTipo.ONG:

                userResult = await _ongHandler.DeleteAsync(request);
                if (userResult.IsFailed)
                    return userResult.Errors;

                identityResult = await _identityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
                if (identityResult.IsFailed)
                    return identityResult.Errors;
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelDeletarUsuario;
        }

        Result commitResult = await _unitOfWork.CommitAsync();

        return commitResult.IsSuccess ? Result.Ok() : commitResult.Errors;
    }

    private Result<EUsuarioTipo?> IdentificarTipoUsuario(Guid id, bool tenantFiltro)
    {
        if (tenantFiltro
            ? _unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
            : _unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any())
        {
            return EUsuarioTipo.Adotante;
        }
        if (tenantFiltro
            ? _unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
            : _unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any())
        {
            return EUsuarioTipo.ONG;
        }
        //return Result.Failure<EUsuarioTipo?>();
        return Error.NullValue;
    }
}
