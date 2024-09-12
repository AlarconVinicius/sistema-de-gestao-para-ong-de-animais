using SGONGA.WebAPI.Business.Requests.Shared;

namespace SGONGA.WebAPI.Business.Responses;

public class AdotanteResponse : UsuarioResponse
{
    public AdotanteResponse() { }

    public AdotanteResponse(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoResponse contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, DateTime createdAt, DateTime updatedAt) : base(id, tenantId, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre, createdAt, updatedAt)
    {
    }
}
