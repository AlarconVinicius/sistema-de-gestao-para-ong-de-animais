using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;
public abstract class UsuarioResponse : Response
{
    [DisplayName("Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; } = Guid.NewGuid();

    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [DisplayName("Apelido")]
    public string Apelido { get; set; } = string.Empty;

    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    [DisplayName("Site")]
    public string Site { get; set; } = string.Empty;

    public ContatoResponse Contato { get; set; } = null!;

    [DisplayName("Whatsapp Visível")]
    public bool TelefoneVisivel { get; set; }

    [DisplayName("Assinar Newsletter")]
    public bool AssinarNewsletter { get; set; }

    [DisplayName("Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [DisplayName("Sobre")]
    public string? Sobre { get; set; } = string.Empty;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    protected UsuarioResponse() { }

    protected UsuarioResponse(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoResponse contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        TenantId = tenantId;
        Nome = nome;
        Apelido = apelido;
        Documento = documento;
        Site = site;
        Contato = contato;
        TelefoneVisivel = telefoneVisivel;
        AssinarNewsletter = assinarNewsletter;
        DataNascimento = dataNascimento;
        Estado = estado;
        Cidade = cidade;
        Sobre = sobre;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
