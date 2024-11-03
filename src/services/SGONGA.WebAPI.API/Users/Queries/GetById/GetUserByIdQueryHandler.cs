using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Users.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Users.Responses;

namespace SGONGA.WebAPI.API.Users.Queries.GetById;

public class GetUserByIdQueryHandler(IUserRepository UserRepository, ITenantProvider TenantProvider) : IQueryHandler<GetUserByIdQuery, PersonResponse>
{
    public async Task<Result<PersonResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = null;
        if (request.TenantFiltro)
        {
            Result<Guid> tenantResult = await TenantProvider.GetTenantId();
            if (tenantResult.IsFailed)
                return tenantResult.Errors;
            tenantId = tenantResult.Value;
        }

        var userType = await UserRepository.IdentifyUserType(request.Id, tenantId, cancellationToken);
        if (userType.IsFailed)
            return userType.Errors;

        switch (userType.Value)
        {
            case EUsuarioTipo.Adotante:
                return await UserRepository.GetAdopterByIdAsync(request.Id, tenantId, cancellationToken);

            case EUsuarioTipo.ONG:
                return await UserRepository.GetNGOByIdAsync(request.Id, tenantId, cancellationToken);

            default:
                return UsuarioErrors.NaoFoiPossivelRecuperarUsuario;
        }
    }
}
