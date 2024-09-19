using SGONGA.Core.Notifications;
using SGONGA.Core.User;
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

    public async Task<UsuarioResponse> GetByIdAsync(GetUsuarioByIdRequest request)
    {
        UsuarioResponse? response;
        try
        {
            var usuarioTipo = IdentificarTipoUsuario(request.Id, request.TenantFiltro);
            if (usuarioTipo is null)
            {
                Notify("Usuário não encontrado.");
                return null!;
            }
            switch (usuarioTipo)
            {
                case EUsuarioTipo.Adotante:
                    response = await _adotanteHandler.GetByIdAsync(request);
                    break;

                case EUsuarioTipo.ONG:
                    response = await _ongHandler.GetByIdAsync(request);
                    break;
                default:
                    Notify("Usuário não encontrado.");
                    return null!;
            }

            if (!IsOperationValid()) return null!;

            return response;
        }
        catch
        {
            Notify("Não foi possível recuperar o usuário.");
            return null!;
        }
    }

    public async Task<PagedResponse<UsuarioResponse>> GetAllAsync(GetAllUsuariosRequest request)
    {
        PagedResponse<UsuarioResponse> response;
        try
        {
            switch (request.UsuarioTipo)
            {
                case EUsuarioTipo.Adotante:
                    response = await _adotanteHandler.GetAllAsync(request);
                    break;

                case EUsuarioTipo.ONG:
                    response = await _ongHandler.GetAllAsync(request);
                    break;
                default:
                    Notify("Nenhum usuário encontrado.");
                    return null!;
            }

            if (!IsOperationValid()) return null!;

            return response;
        }
        catch
        {
            Notify("Não foi possível recuperar os usuários.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateUsuarioRequest request)
    {
        try
        {
            switch (request.UsuarioTipo)
            {
                case EUsuarioTipo.Adotante:
                    await _adotanteHandler.CreateAsync(request);

                    if (!IsOperationValid()) return;

                    await _identityHandler.CreateAsync(new CreateUserRequest(request.Id, request.Contato.Email, request.Senha, request.ConfirmarSenha));

                    if (!IsOperationValid()) return;

                    await _identityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Id, new UserClaim("Tenant", request.TenantId.ToString())));
                    break;

                case EUsuarioTipo.ONG:
                    await _ongHandler.CreateAsync(request);

                    if (!IsOperationValid()) return;

                    await _identityHandler.CreateAsync(new CreateUserRequest(request.Id, request.Contato.Email, request.Senha, request.ConfirmarSenha));

                    if (!IsOperationValid()) return;

                    await _identityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Id, new UserClaim("Tenant", request.TenantId.ToString())));
                    break;
                default:
                    Notify("Não foi possível criar o usuário.");
                    return;
            }

            if (!IsOperationValid()) return;

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível criar o usuário.");
            return;
        }
    }

    public async Task UpdateAsync(UpdateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new AdotanteValidation(), adotante)) return;

        try
        {
            switch (request.UsuarioTipo)
            {
                case EUsuarioTipo.Adotante:
                    await _adotanteHandler.UpdateAsync(request);
                    break;

                case EUsuarioTipo.ONG:
                    await _ongHandler.UpdateAsync(request);
                    break;
                default:
                    Notify("Não foi possível atualizar o usuário.");
                    return;
            }

            if (!IsOperationValid()) return;

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível atualizar o usuário.");
            return;
        }
    }

    public async Task DeleteAsync(DeleteUsuarioRequest request)
    {
        try
        {
            var usuarioTipo = IdentificarTipoUsuario(request.Id, true);
            if (usuarioTipo is null)
            {
                Notify("Usuário não encontrado.");
                return;
            }
            switch (usuarioTipo)
            {
                case EUsuarioTipo.Adotante:
                    await _adotanteHandler.DeleteAsync(request);

                    if (!IsOperationValid()) return;

                    await _identityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
                    break;

                case EUsuarioTipo.ONG:
                    await _ongHandler.DeleteAsync(request);

                    if (!IsOperationValid()) return;

                    await _identityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
                    break;
                default:
                    Notify("Não foi possível deletar o usuário.");
                    return;
            }

            if (!IsOperationValid()) return;

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível deletar o usuário.");
            return;
        }
    }

    private EUsuarioTipo? IdentificarTipoUsuario(Guid id, bool tenantFiltro)
    {
        if (tenantFiltro
            ? _unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == id).Result.Any()
            : _unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Any())
        {
            return EUsuarioTipo.Adotante;
        }
        if (tenantFiltro
            ? _unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Any()
            : _unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Any())
        {
            return EUsuarioTipo.ONG;
        }
        return null!;
    }
}
