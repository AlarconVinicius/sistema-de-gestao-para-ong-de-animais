using SGONGA.WebAPI.API.Animals.Queries;
using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Shared;

public static class AnimalMappings
{
    public static AnimalResponse MapDomainToResponse(this Animal request)
    {
        if (request == null)
        {
            return null!;
        }
        string endereco = $"{request.ONG!.Cidade}, {request.ONG.Estado}";

        return new AnimalResponse(request.Id, request.TenantId, request.Nome, request.Especie, request.Raca, request.Sexo, request.Castrado, request.Cor, request.Porte, request.Idade, request.ONG.Nome, endereco, request.Descricao, request.Observacao, request.ChavePix, request.Foto, request.CreatedAt, request.UpdatedAt);
    }
    public static BasePagedResponse<AnimalResponse> MapDomainToResponse(this PagedResult<Animal> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new BasePagedResponse<AnimalResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }
}
