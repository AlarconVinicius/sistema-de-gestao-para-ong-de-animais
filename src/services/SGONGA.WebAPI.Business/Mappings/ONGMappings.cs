using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class ONGMappings
{
    public static ONGResponse MapDomainToResponse(this ONG request)
    {
        if (request == null)
        {
            return null!;
        }

        return new ONGResponse(request.Id, request.Nome, request.Descricao, request.Contato.Telefone.Numero, request.Contato.Email.Endereco, request.ChavePix, request.CreatedAt, request.UpdatedAt, request.Endereco.MapDomainToResponse());
    }

    public static ONG MapResponseToDomain(this ONGResponse request)
    {
        if (request == null)
        {
            return null!;
        }

        return new ONG(request.Id, request.Nome, request.Descricao, request.Telefone, request.Email, request.ChavePix);
    }

    public static PagedResponse<ONGResponse> MapDomainToResponse(this PagedResult<ONG> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<ONGResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }

    public static ONG MapRequestToDomain(this CreateONGRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new ONG(request.Nome, request.Descricao, request.Telefone, request.Email, request.ChavePix);
    }
}
