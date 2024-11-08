using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.People.Queries.GetById;

public class GetPersonByIdQueryHandler(IPersonRepository PersonRepository, ITenantProvider TenantProvider) : IQueryHandler<GetPersonByIdQuery, PersonResponse>
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

        var personType = await PersonRepository.IdentifyPersonType(request.Id, tenantId, cancellationToken);
        if (personType.IsFailed)
            return personType.Errors;

        switch (personType.Value)
        {
            case EPersonType.Adopter:
                return await PersonRepository.GetAdopterByIdAsync(request.Id, tenantId, cancellationToken);

            case EPersonType.Organization:
                return await PersonRepository.GetOrganizationByIdAsync(request.Id, tenantId, cancellationToken);

            default:
                return PersonErrors.NaoFoiPossivelRecuperarUsuario;
        }
    }
}
