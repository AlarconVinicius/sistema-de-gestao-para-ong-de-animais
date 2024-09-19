using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateONGRequest : CreateUsuarioRequest
{
    public CreateONGRequest() { }

    public CreateONGRequest(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix) : base(id, tenantId, EUsuarioTipo.ONG, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre, chavePix)
    {
    }
}

public class UpdateONGRequest : UpdateUsuarioRequest
{
    public UpdateONGRequest() { }

    public UpdateONGRequest(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix) : base(id, tenantId, EUsuarioTipo.ONG, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre, chavePix)
    {
    }
}

public class GetAllONGsRequest : PagedRequest
{
    public bool TenantFiltro { get; set; }
    public GetAllONGsRequest() { }

    public GetAllONGsRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFiltro = false) : base(pageSize, pageNumber, query, returnAll)
    {
        TenantFiltro = tenantFiltro;
    }
}

public class GetONGByIdRequest : Request
{
    public bool TenantFiltro { get; set; }
    public Guid Id { get; set; }

    public GetONGByIdRequest() { }

    public GetONGByIdRequest(Guid id, bool tenantFiltro = false)
    {
        Id = id;
        TenantFiltro = tenantFiltro;
    }
}

public class DeleteONGRequest : Request
{
    public Guid Id { get; set; }

    public DeleteONGRequest() { }

    public DeleteONGRequest(Guid id)
    {
        Id = id;
    }
}
