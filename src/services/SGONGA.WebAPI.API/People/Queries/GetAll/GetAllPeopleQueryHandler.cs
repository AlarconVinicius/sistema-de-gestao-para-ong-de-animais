using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.People.Queries.GetAll;

public class GetAllPeopleQueryHandler(IPersonRepository UserRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAllPeopleQuery, BasePagedResponse<PersonResponse>>
{
    public async Task<Result<BasePagedResponse<PersonResponse>>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = null;
        if (request.TenantFilter)
        {
            Result<Guid> tenantResult = await TenantProvider.GetTenantId();
            if (tenantResult.IsFailed)
                return tenantResult.Errors;
            tenantId = tenantResult.Value;
        }
        switch (request.PersonType)
        {
            case EPersonType.Adopter:
                return await UserRepository.GetAllAdoptersPagedAsync(tenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll, cancellationToken);

            case EPersonType.NGO:
                return await UserRepository.GetAllNGOsPagedAsync(tenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll, cancellationToken);
            default:
                return PersonErrors.NaoFoiPossivelRecuperarUsuarios;
        }
    }
}
