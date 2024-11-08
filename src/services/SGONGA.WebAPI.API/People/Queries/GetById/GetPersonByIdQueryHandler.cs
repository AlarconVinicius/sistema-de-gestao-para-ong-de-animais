using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.People.Queries.GetById;

public class GetPersonByIdQueryHandler(IPersonRepository UserRepository, ITenantProvider TenantProvider) : IQueryHandler<GetPersonByIdQuery, PersonResponse>
{
    public async Task<Result<PersonResponse>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
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
            case EPersonType.Adopter:
                return await UserRepository.GetAdopterByIdAsync(request.Id, tenantId, cancellationToken);

            case EPersonType.NGO:
                return await UserRepository.GetNGOByIdAsync(request.Id, tenantId, cancellationToken);

            default:
                return PersonErrors.NaoFoiPossivelRecuperarUsuario;
        }
    }
}
