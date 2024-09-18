using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateAdotanteRequest : CreateUsuarioRequest
{
    public CreateAdotanteRequest() { }

    public CreateAdotanteRequest(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre) : base(id, tenantId, EUsuarioTipo.Adotante, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
    }
}

public class UpdateAdotanteRequest : UpdateUsuarioRequest
{
    public UpdateAdotanteRequest() { }

    public UpdateAdotanteRequest(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre) : base(id, tenantId, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
    }
}

public class GetAllAdotantesRequest : PagedRequest
{
    public GetAllAdotantesRequest() { }

    public GetAllAdotantesRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false) : base(pageSize, pageNumber, query, returnAll)
    {

    }
}

public class GetAdotanteByIdRequest : Request
{
    public Guid Id { get; set; }

    public GetAdotanteByIdRequest() { }

    public GetAdotanteByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteAdotanteRequest : Request
{
    public Guid Id { get; set; }

    public DeleteAdotanteRequest() { }

    public DeleteAdotanteRequest(Guid id)
    {
        Id = id;
    }
}
