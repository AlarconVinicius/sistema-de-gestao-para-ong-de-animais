using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;

namespace SGONGA.WebAPI.API.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(IUserRepository UserRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAllUsersQuery, BasePagedResponse<PersonResponse>>
{
    public async Task<Result<BasePagedResponse<PersonResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = null;
        if (request.TenantFiltro)
        {
            Result<Guid> tenantResult = await TenantProvider.GetTenantId();
            if (tenantResult.IsFailed)
                return tenantResult.Errors;
            tenantId = tenantResult.Value;
        }
        switch (request.UsuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                return await UserRepository.GetAllAdoptersPagedAsync(tenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll, cancellationToken);

            case EUsuarioTipo.ONG:
                return await UserRepository.GetAllNGOsPagedAsync(tenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll, cancellationToken);
            default:
                return UsuarioErrors.NaoFoiPossivelRecuperarUsuarios;
        }
    }
}
