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

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    [DisplayName("Senha")]
    public string Senha { get; set; } = string.Empty;

    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    [DisplayName("Confirmar Senha")]
    public string ConfirmarSenha { get; set; } = string.Empty;

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

    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; } = string.Empty;
    public CreateUsuarioRequest() { }

    public CreateUsuarioRequest(Guid id, Guid tenantId, EUsuarioTipo usuarioTipo, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix)
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
        ChavePix = chavePix;
    }
}

public class UpdateUsuarioRequest : Request
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

    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; } = string.Empty;

    public UpdateUsuarioRequest() { }

    public UpdateUsuarioRequest(Guid id, Guid tenantId, EUsuarioTipo usuarioTipo, string nome, string apelido, string documento, string site, ContatoRequest contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix)
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
        ChavePix = chavePix;
    }
}

public class GetAllUsuariosRequest : PagedRequest
{

    [DisplayName("Tipo de Usuário")]
    public EUsuarioTipo UsuarioTipo { get; set; }

    [DisplayName("Filtro por Tenant")]
    public bool TenantFiltro { get; set; }
    public GetAllUsuariosRequest() { }

    public GetAllUsuariosRequest(int usuarioTipo, int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFiltro = false) : base(pageSize, pageNumber, query, returnAll)
    {
        UsuarioTipo = (EUsuarioTipo)usuarioTipo;
        TenantFiltro = tenantFiltro;
    }
}

public class GetUsuarioByIdRequest : Request
{
    public Guid Id { get; set; }

    [DisplayName("Filtro por Tenant")]
    public bool TenantFiltro { get; set; }

    public GetUsuarioByIdRequest() { }

    public GetUsuarioByIdRequest(Guid id, bool tenantFiltro = false)
    {
        Id = id;
        TenantFiltro = tenantFiltro;
    }
}

public class DeleteUsuarioRequest : Request
{
    public Guid Id { get; set; }

    public DeleteUsuarioRequest() { }

    public DeleteUsuarioRequest(Guid id)
    {
        Id = id;
    }
}