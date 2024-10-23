using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

internal sealed class GetAnimalByIdQueryHandler(IONGDbContext Context, ITenantProvider TenantProvider) : IQueryHandler<GetAnimalByIdQuery,AnimalResponse>
{
    public async Task<Result<AnimalResponse>> Handle(GetAnimalByIdQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Animais
            .Include(q => q.ONG)
            .AsNoTracking()
            .AsQueryable();

        if (query.TenantFiltro)
        {
            Result<Guid> tenantId = await TenantProvider.GetTenantId();
            if (tenantId.IsFailed)
                return tenantId.Errors;

            queryable = queryable.Where(q => q.TenantId == tenantId.Value);
        }

        var animal = await queryable
            .Where(animal => animal.Id == query.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (animal is null)
            return AnimalErrors.AnimalNotFound(query.Id);

        AnimalResponse animalResponse = new(
                animal.Id,
                animal.TenantId,
                animal.Nome,
                animal.Especie,
                animal.Raca,
                animal.Sexo,
                animal.Castrado,
                animal.Cor,
                animal.Porte,
                animal.Idade,
                animal.ONG!.Nome,
                animal.ONG.Estado + ", " + animal.ONG.Cidade,
                animal.Descricao,
                animal.Observacao,
                animal.ChavePix,
                animal.Foto,
                animal.CreatedAt,
                animal.UpdatedAt
            );

        return animalResponse;
    }
}
