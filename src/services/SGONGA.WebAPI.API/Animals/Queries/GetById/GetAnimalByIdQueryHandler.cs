using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

internal sealed class GetAnimalByIdQueryHandler(IONGDbContext Context, TenantProvider TenantProvider) : IQueryHandler<GetAnimalByIdQuery,AnimalResponse>
{
    public async Task<Result<AnimalResponse>> Handle(GetAnimalByIdQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Animais
            .Include(q => q.ONG)
            .AsNoTracking()
            .AsQueryable();

        if (query.TenantFiltro)
        {
            queryable = queryable.Where(q => q.TenantId == TenantProvider.TenantId);
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
