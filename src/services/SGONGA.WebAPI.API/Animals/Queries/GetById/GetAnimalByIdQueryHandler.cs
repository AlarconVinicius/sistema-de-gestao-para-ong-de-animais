﻿using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Errors;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

internal sealed class GetAnimalByIdQueryHandler(IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : IQueryHandler<GetAnimalByIdQuery,AnimalResponse>
{
    public async Task<Result<AnimalResponse>> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = null;
        if (request.TenantFilter)
        {
            Result<Guid> tenantResult = await TenantProvider.GetTenantId();
            if (tenantResult.IsFailed)
                return tenantResult.Errors;
            tenantId = tenantResult.Value;
        }

        AnimalResponse result = await AnimalRepository.GetByIdAsync(request.Id, tenantId, cancellationToken);

        return result is not null ? result : AnimalErrors.AnimalNotFound(request.Id);
    }
}
