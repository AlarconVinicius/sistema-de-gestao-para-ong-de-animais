﻿using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class ONGMappings
{
    //public static ONGResponse MapDomainToResponse(this ONG request)
    //{
    //    if (request == null)
    //    {
    //        return null!;
    //    }

    //    return new ONGResponse(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapDomainToResponse(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, request.ChavePix, request.CreatedAt, request.UpdatedAt);
    //}

    //public static PagedResponse<ONGResponse> MapDomainToResponse(this PagedResult<ONG> request)
    //{
    //    if (request == null)
    //    {
    //        return null!;
    //    }

    //    return new PagedResponse<ONGResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    //}

    //public static ONG MapRequestToDomain(this CreateONGRequest request)
    //{
    //    if (request == null)
    //    {
    //        return null!;
    //    }

    //    return new ONG(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato.MapRequestToDomain(), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, request.ChavePix);
    //}
}
