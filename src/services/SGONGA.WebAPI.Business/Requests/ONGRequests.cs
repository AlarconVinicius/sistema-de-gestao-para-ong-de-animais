using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateONGRequest : Request
{
    [DisplayName("Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Instagram")]
    public string Instagram { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; } = string.Empty;

    public ContatoRequest Contato { get; set; } = null!;

    public EnderecoRequest Endereco { get; set; } = null!;

    public CreateONGRequest() { }

    public CreateONGRequest(Guid id, string nome, string instagram, string documento, string? chavePix, ContatoRequest contato, EnderecoRequest endereco)
    {
        Id = id;
        Nome = nome;
        Instagram = instagram;
        Documento = documento;
        ChavePix = chavePix;
        Contato = contato;
        Endereco = endereco;
    }
}

public class UpdateONGRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Instagram")]
    public string Instagram { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; }

    public ContatoRequest Contato { get; set; } = null!;

    public EnderecoRequest Endereco { get; set; } = null!;

    public UpdateONGRequest() { }

    public UpdateONGRequest(Guid id, string nome, string instagram, string? chavePix, ContatoRequest contato, EnderecoRequest endereco)
    {
        Id = id;
        Nome = nome;
        Instagram = instagram;
        ChavePix = chavePix;
        Contato = contato;
        Endereco = endereco;
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
