using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Animals.Queries.GetAll;

internal sealed class GetAllAnimalsQueryHandler(IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAllAnimalsQuery, BasePagedResponse<AnimalResponse>>
{
    public async Task<Result<BasePagedResponse<AnimalResponse>>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = null;
        if (request.TenantFiltro)
        {
            Result<Guid> tenantResult = await TenantProvider.GetTenantId();
            if (tenantResult.IsFailed)
                return tenantResult.Errors;
            tenantId = tenantResult.Value;
        }
        return await AnimalRepository.GetAllAsync(tenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll, cancellationToken);
    }
}