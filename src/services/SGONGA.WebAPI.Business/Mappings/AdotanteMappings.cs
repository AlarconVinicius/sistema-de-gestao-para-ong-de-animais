using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class AdotanteMappings
{
    public static AdotanteResponse MapDomainToResponse(this Adotante request)
    {
        if (request == null)
        {
            return null!;
        }

        return new AdotanteResponse(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapDomainToResponse(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, request.CreatedAt, request.UpdatedAt);
    }

    public static PagedResponse<AdotanteResponse> MapDomainToResponse(this PagedResult<Adotante> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<AdotanteResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }

    public static Adotante MapRequestToDomain(this CreateAdotanteRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Adotante(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapRequestToDomain(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre);
    }
}
