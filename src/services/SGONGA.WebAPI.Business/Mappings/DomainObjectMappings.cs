﻿using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Requests.Shared;

namespace SGONGA.WebAPI.Business.Mappings;

public static class DomainObjectMappings
{
    public static EnderecoResponse MapDomainToResponse(this Endereco request)
    {
        if (request == null)
        {
            return null!;
        }

        return new EnderecoResponse(request.Cidade, request.Estado, request.CEP, request.Logradouro, request.Bairro, request.Numero, request.Complemento, request.Referencia);
    }

    public static Endereco MapRequestToDomain(this EnderecoRequest request)
    {
        if (request == null)
        {
            return null!;
        }

        return new Endereco(request.Cidade, request.Estado, request.CEP, request.Logradouro, request.Bairro, request.Numero, request.Complemento, request.Referencia);
    }
}