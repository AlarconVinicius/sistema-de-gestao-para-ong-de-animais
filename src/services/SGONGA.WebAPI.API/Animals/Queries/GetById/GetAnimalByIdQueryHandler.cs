using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

internal sealed class GetAnimalByIdQueryHandler(IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAnimalByIdQuery,AnimalResponse>
{
    public async Task<Result<AnimalResponse>> Handle(GetAnimalByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.TenantFiltro)
        {
            Result<Guid> tenantId = await TenantProvider.GetTenantId();
            if (tenantId.IsFailed)
                return tenantId.Errors;

            return await AnimalRepository.GetByIdAsync(query.Id, tenantId.Value);
        }
        return await AnimalRepository.GetByIdAsync(query.Id, null);
    }
}
