using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class DomainObjectMappings
{
    public static EnderecoResponse MapDomainToResponse(this Endereco request)
    {
        if (request == null)
        {
            return null!;
        }

        return new EnderecoResponse(request.Rua, request.Cidade, request.Estado, request.CEP, request.Complemento);
    }
}