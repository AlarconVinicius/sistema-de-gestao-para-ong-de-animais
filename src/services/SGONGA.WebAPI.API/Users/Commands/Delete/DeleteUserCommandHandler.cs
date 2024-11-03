using SGONGA.Core.User;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Users.Commands.Delete;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Animals.Commands.Delete;

internal sealed class DeleteUserCommandHandler(IGenericUnitOfWork UnitOfWork, IUserRepository UserRepository, ITenantProvider TenantProvider, IIdentityHandler IdentityHandler, IAspNetUser AppUser) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (AppUser.GetUserId() != request.Id && EhSuperAdmin().IsFailed)
            return Error.AccessDenied;

        Result<Guid> tenantResult = await TenantProvider.GetTenantId();
        if (tenantResult.IsFailed)
            return tenantResult.Errors;

        Guid tenantId = tenantResult.Value;

        var userType = await UserRepository.IdentifyUserType(request.Id, tenantId, cancellationToken);
        if (userType.IsFailed)
            return userType.Errors;

        Result identityResult;
        switch (userType.Value)
        {
            case EUsuarioTipo.Adotante:
                UnitOfWork.Delete(Adopter.Create(request.Id));
                break;

            case EUsuarioTipo.ONG:
                var NGOResult = await UserRepository.GetNGOByIdWithAnimalsAsync(request.Id, tenantId, cancellationToken);

                UnitOfWork.DeleteRange(NGOResult.Animais);
                UnitOfWork.Delete(NGOResult);
                break;
            default:
                return UsuarioErrors.NaoFoiPossivelDeletarUsuario;
        }

        identityResult = await IdentityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
        if (identityResult.IsFailed)
            return identityResult.Errors;

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    private Result EhSuperAdmin()
    {
        if (AppUser.HasClaim("Permissions", "SuperAdmin"))
        {
            return Result.Ok();
        }
        return Error.NullValue;
    }
}
