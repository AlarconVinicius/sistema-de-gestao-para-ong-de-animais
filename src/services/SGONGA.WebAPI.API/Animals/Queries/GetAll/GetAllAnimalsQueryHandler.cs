using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Shared;
using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Queries.GetAll;

internal sealed class GetAllAnimalsQueryHandler(IUnitOfWork UnitOfWork, TenantProvider TenantProvider) : IQueryHandler<GetAllAnimalsQuery, BasePagedResponse<AnimalResponse>>
{
    public async Task<Result<BasePagedResponse<AnimalResponse>>> Handle(GetAllAnimalsQuery query, CancellationToken cancellationToken)
    {
        //var tenantId = TenantProvider.TenantId
        //    ?? throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        var results = query.TenantFiltro
                           ? await UnitOfWork.AnimalRepository.GetAllPagedAsync(null, query.PageNumber, query.PageSize, query.Query, query.ReturnAll)
                           : await UnitOfWork.AnimalRepository.GetAllPagedWithoutTenantAsync(null, query.PageNumber, query.PageSize, query.Query, query.ReturnAll);

        return results.Value.MapDomainToResponse();
    }
}
