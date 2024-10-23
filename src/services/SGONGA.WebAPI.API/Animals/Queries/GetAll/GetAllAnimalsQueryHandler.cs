using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Animals.Queries.GetAll;

internal sealed class GetAllAnimalsQueryHandler(IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAllAnimalsQuery, BasePagedResponse<AnimalResponse>>
{
    public async Task<Result<BasePagedResponse<AnimalResponse>>> Handle(GetAllAnimalsQuery query, CancellationToken cancellationToken)
    {
        if (query.TenantFiltro)
        {
            Result<Guid> tenantId = await TenantProvider.GetTenantId();
            if (tenantId.IsFailed)
                return tenantId.Errors;

            return await AnimalRepository.GetAllAsync(query.PageNumber, query.PageSize, query.Query, tenantId.Value, query.ReturnAll, cancellationToken);
        }
        return await AnimalRepository.GetAllAsync(query.PageNumber, query.PageSize, query.Query, null, query.ReturnAll, cancellationToken);
    }
}