using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class UsuarioMappings
{
    public static UsuarioResponse MapAdotanteDomainToResponse(this Adotante request)
    {
        if (request == null)
        {
            return null!;
        }

        return new UsuarioResponse(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapDomainToResponse(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, null, request.CreatedAt, request.UpdatedAt);
    }
    public static PagedResponse<UsuarioResponse> MapAdotanteDomainToResponse(this PagedResult<Adotante> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<UsuarioResponse>(request.List.Select(x => x.MapAdotanteDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }
    public static Adotante MapRequestToAdotanteDomain(this CreateUsuarioRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Adotante(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapRequestToDomain(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre);
    }

    public static UsuarioResponse MapONGDomainToResponse(this ONG request)
    {
        if (request == null)
        {
            return null!;
        }

        return new UsuarioResponse(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapDomainToResponse(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, request.ChavePix, request.CreatedAt, request.UpdatedAt);
    }
    public static PagedResponse<UsuarioResponse> MapONGDomainToResponse(this PagedResult<ONG> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<UsuarioResponse>(request.List.Select(x => x.MapONGDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }
    public static ONG MapRequestToONGDomain(this CreateUsuarioRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return ONG.Create(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapRequestToDomain(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, request.ChavePix);
    }
}
