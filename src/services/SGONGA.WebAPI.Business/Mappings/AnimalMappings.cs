using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class AnimalMappings
{
    public static AnimalResponse MapDomainToResponse(this Animal request)
    {
        if (request == null)
        {
            return null!;
        }
        string endereco = $"{request.ONG!.Endereco.Cidade}, {request.ONG.Endereco.Estado}";

        return new AnimalResponse(request.Id, request.TenantId, request.Nome, request.Especie, request.Raca, request.Sexo, request.Castrado, request.Cor, request.Porte, request.Idade, request.ONG.Nome, endereco, request.Descricao, request.Observacao, request.ChavePix, request.Foto, request.CreatedAt, request.UpdatedAt);
    }

    public static Animal MapResponseToDomain(this AnimalResponse request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Animal(request.Id, request.ONGId, request.Nome, request.Especie, request.Raca, request.Sexo, request.Castrado, request.Cor, request.Porte, request.Idade, request.Descricao, request.Observacao, request.Foto, request.ChavePix);
    }

    public static PagedResponse<AnimalResponse> MapDomainToResponse(this PagedResult<Animal> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<AnimalResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }

    public static Animal MapRequestToDomain(this CreateAnimalRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Animal(request.Nome, request.Especie, request.Raca, request.Sexo, request.Castrado, request.Cor, request.Porte, request.Idade, request.Descricao, request.Observacao, request.Foto, request.ChavePix);
    }
}
