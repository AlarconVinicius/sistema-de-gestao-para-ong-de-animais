using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Requests.Shared;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Mappings;

public static class SolicitacaoCadastroMappings
{
    public static SolicitacaoCadastroResponse MapDomainToResponse(this SolicitacaoCadastro request)
    {
        if (request == null)
        {
            return null!;
        }
        //ContatoRequest contatoOng = new ContatoRequest(request.ContatoOng.Telefone.Numero, request.ContatoOng.Email.Endereco);
        //EnderecoRequest enderecoOng = new EnderecoRequest(request.EnderecoOng.Cidade, request.EnderecoOng.Estado, request.EnderecoOng.CEP, request.EnderecoOng.Logradouro, request.EnderecoOng.Bairro, request.EnderecoOng.Numero, request.EnderecoOng.Complemento, request.EnderecoOng.Referencia);
        //CreateONGRequest ong = new CreateONGRequest(request.IdOng, request.NomeOng, request.InstagramOng, request.DocumentoOng, request.ChavePixOng, contatoOng, enderecoOng);

        //ContatoRequest contatoResponsavel = new ContatoRequest(request.ContatoResponsavel.Telefone.Numero, request.ContatoOng.Email.Endereco);
        //CreateAdotanteRequest colaborador = new CreateAdotanteRequest(request.IdResponsavel, request.IdOng, request.NomeResponsavel, request.DocumentoResponsavel, request.DataNascimentoResponsavel, contatoResponsavel);
        //return new SolicitacaoCadastroResponse(request.Id, ong, colaborador, request.Status, request.CreatedAt, request.UpdatedAt);
        return new SolicitacaoCadastroResponse();
    }

    public static PagedResponse<SolicitacaoCadastroResponse> MapDomainToResponse(this PagedResult<SolicitacaoCadastro> request)
    {
        if (request == null)
        {
            return null!;
        }

        return new PagedResponse<SolicitacaoCadastroResponse>(request.List.Select(x => x.MapDomainToResponse()).ToList(), request.TotalResults, request.PageIndex, request.PageSize, request.Query, request.ReturnAll);
    }

    public static SolicitacaoCadastro MapRequestToDomain(this CreateSolicitacaoCadastroRequests request)
    {
        if (request == null)
        {
            return null!;
        }

        //return new SolicitacaoCadastro(request.ONG.Id, request.ONG.Nome, request.ONG.Instagram, request.ONG.Documento, request.ONG.Contato.MapRequestToDomain(), request.ONG.Endereco.MapRequestToDomain(), request.ONG.ChavePix, request.Responsavel.Id, request.Responsavel.Nome, request.Responsavel.Documento, request.Responsavel.Contato.MapRequestToDomain(), request.Responsavel.DataNascimento, request.Status);
        return new SolicitacaoCadastro();
    }
}
