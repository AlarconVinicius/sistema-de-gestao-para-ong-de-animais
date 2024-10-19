using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Shared;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

internal sealed class GetAnimalByIdQueryHandler(IUnitOfWork UnitOfWork, TenantProvider TenantProvider) : IQueryHandler<GetAnimalByIdQuery,AnimalResponse>
{
    public async Task<Result<AnimalResponse>> Handle(GetAnimalByIdQuery query, CancellationToken cancellationToken)
    {
        if (AnimalExiste(query.Id, query.TenantFiltro).IsFailed)
            return null!;

        var result = query.TenantFiltro
                           ? await UnitOfWork.AnimalRepository.GetByIdAsync(query.Id)
                           : await UnitOfWork.AnimalRepository.GetByIdWithoutTenantAsync(query.Id);

        return result.Value.MapDomainToResponse();
    }
    private Result AnimalExiste(Guid id, bool tenantFiltro)
    {
        var exists = tenantFiltro
            ? UnitOfWork.AnimalRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
            : UnitOfWork.AnimalRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any();

        return exists ? Result.Ok() : Error.NullValue;
    }
}
