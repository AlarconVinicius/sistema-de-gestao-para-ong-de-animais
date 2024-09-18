using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;
public class CreateUsuarioRequest : Request
{
    [DisplayName("Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; } = Guid.NewGuid();

    [DisplayName("Tipo de Usuário")]
    public EUsuarioTipo UsuarioTipo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Apelido")]
    public string Apelido { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Url(ErrorMessage = "URL inválida.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Site")]
    public string Site { get; set; } = string.Empty;

    public ContatoRequest Contato { get; set; } = null!;

    [DisplayName("Whatsapp Visível")]
    public bool TelefoneVisivel { get; set; }

    [DisplayName("Assinar Newsletter")]
    public bool AssinarNewsletter { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Sobre")]
    public string? Sobre { get; set; } = string.Empty;
    public CreateUsuarioRequest() { }

    public CreateUsuarioRequest(Guid id, Guid tenantId, EUsuarioTipo usuarioTipo, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre)
    {
        Id = id;
        TenantId = tenantId;
        UsuarioTipo = usuarioTipo;
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
    }
}

public abstract class UpdateUsuarioRequest : Request
{
    [DisplayName("Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Apelido")]
    public string Apelido { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Url(ErrorMessage = "URL inválida.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Site")]
    public string Site { get; set; } = string.Empty;

    public ContatoRequest Contato { get; set; } = null!;

    [DisplayName("Whatsapp Visível")]
    public bool TelefoneVisivel { get; set; }

    [DisplayName("Assinar Newsletter")]
    public bool AssinarNewsletter { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Sobre")]
    public string? Sobre { get; set; } = string.Empty;
    protected UpdateUsuarioRequest() { }

    protected UpdateUsuarioRequest(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre)
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
    }
}