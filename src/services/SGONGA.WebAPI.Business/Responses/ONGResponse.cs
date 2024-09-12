using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;

public class ONGResponse : UsuarioResponse
{
    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; } = string.Empty;

    public ONGResponse() { }

    public ONGResponse(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoResponse contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix, DateTime createdAt, DateTime updatedAt) : base(id, tenantId, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre, createdAt, updatedAt)
    {
        ChavePix = chavePix;
    }
}
