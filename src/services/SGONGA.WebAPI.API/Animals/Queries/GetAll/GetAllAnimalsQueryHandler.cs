using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Animals.Queries.GetAll;

internal sealed class GetAllAnimalsQueryHandler(IONGDbContext Context, ITenantProvider TenantProvider) : IQueryHandler<GetAllAnimalsQuery, BasePagedResponse<AnimalResponse>>
{
    public async Task<Result<BasePagedResponse<AnimalResponse>>> Handle(GetAllAnimalsQuery query, CancellationToken cancellationToken)
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
        if (!string.IsNullOrEmpty(query.Query))
        {
            queryable = queryable.Where(q => q.Nome.Contains(query.Query) || q.ONG!.Nome.Contains(query.Query));
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);

        var animalResponses = queryable.Select(animal => new AnimalResponse(
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
        ));

        var totalResults = await queryable.CountAsync(cancellationToken);

        if (!query.ReturnAll)
        {
            animalResponses = animalResponses.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
        }

        var list = await animalResponses.ToListAsync(cancellationToken);

        return new BasePagedResponse<AnimalResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = query.PageNumber,
            PageSize = query.PageSize
        };        
    }
}