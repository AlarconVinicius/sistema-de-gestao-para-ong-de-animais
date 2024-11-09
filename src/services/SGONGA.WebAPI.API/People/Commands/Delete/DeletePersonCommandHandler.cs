using SGONGA.Core.User;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Users.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Users.Requests;

namespace SGONGA.WebAPI.API.People.Commands.Delete;

internal sealed class DeletePersonCommandHandler(IGenericUnitOfWork UnitOfWork, IPersonRepository PersonRepository, ITenantProvider TenantProvider, IIdentityHandler IdentityHandler, IAspNetUser AppUser) : ICommandHandler<DeletePersonCommand>
{
    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        if (AppUser.GetUserId() != request.Id && IsSuperAdmin().IsFailed)
            return Error.AccessDenied;

        Result<Guid> tenantResult = await TenantProvider.GetTenantId();
        if (tenantResult.IsFailed)
            return tenantResult.Errors;

        Guid tenantId = tenantResult.Value;

        var personType = await PersonRepository.IdentifyPersonType(request.Id, tenantId, cancellationToken);
        if (personType.IsFailed)
            return personType.Errors;

        Result identityResult;
        switch (personType.Value)
        {
            case EPersonType.Adopter:
                UnitOfWork.Delete(Adopter.Create(request.Id));
                break;

            case EPersonType.Organization:
                var OrganizationResult = await PersonRepository.GetOrganizationByIdWithAnimalsAsync(request.Id, tenantId, cancellationToken);

                UnitOfWork.DeleteRange(OrganizationResult.Animals);
                UnitOfWork.Delete(OrganizationResult);
                break;
            default:
                return PersonErrors.NaoFoiPossivelDeletarUsuario;
        }

        identityResult = await IdentityHandler.DeleteAsync(new DeleteUserRequest(request.Id));
        if (identityResult.IsFailed)
            return identityResult.Errors;

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    private Result IsSuperAdmin()
    {
        if (AppUser.HasClaim("Permissions", "SuperAdmin"))
        {
            return Result.Ok();
        }
        return Error.NullValue;
    }
}
