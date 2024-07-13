using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class ColaboradorMappings
{
    public static ColaboradorResponse MapDomainToResponse(this Colaborador request)
    {
        if (request == null)
        {
            return null!;
        }

        return new ColaboradorResponse(request.Id, request.TenantId, request.Email.Endereco, request.CreatedAt, request.UpdatedAt);
    }

    public static Colaborador MapResponseToDomain(this ColaboradorResponse request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Colaborador(request.Id, request.TenantId, request.Email);
    }

    public static PagedResponse<ColaboradorResponse> MapDomainToResponse(this PagedResult<Colaborador> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<ColaboradorResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }

    public static Colaborador MapRequestToDomain(this CreateColaboradorRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Colaborador(request.TenantId, request.Email);
    }
}
