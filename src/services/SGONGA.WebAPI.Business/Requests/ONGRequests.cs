using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateONGRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Phone(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(15, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(254, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Rua")]
    public string Rua { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(2, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(9, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("CEP")]
    public string CEP { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Complemento")]
    public string Complemento { get; set; } = string.Empty;

    public CreateONGRequest() { }

    public CreateONGRequest(string nome, string descricao, string chavePix, string telefone, string email, string rua, string cidade, string estado, string cep, string complemento)
    {
        Nome = nome;
        Descricao = descricao;
        ChavePix = chavePix;
        Telefone = telefone;
        Email = email;
        Rua = rua;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Complemento = complemento;
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
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Phone(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(15, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(254, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Rua")]
    public string Rua { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(2, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(9, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("CEP")]
    public string CEP { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Complemento")]
    public string Complemento { get; set; } = string.Empty;

    public UpdateONGRequest() { }

    public UpdateONGRequest(Guid id, string nome, string descricao, string chavePix, string telefone, string email, string rua, string cidade, string estado, string cep, string complemento)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        ChavePix = chavePix;
        Telefone = telefone;
        Email = email;
        Rua = rua;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Complemento = complemento;
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
