using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateColaboradorRequest : Request
{
    [DisplayName("Id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; } = Guid.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    public ContatoRequest Contato { get; set; } = null!;

    public CreateColaboradorRequest() { }

    public CreateColaboradorRequest(Guid id, Guid tenantId, string nome, string documento, DateTime dataNascimento, ContatoRequest contato)
    {
        Id = id;
        TenantId = tenantId;
        Nome = nome;
        Documento = documento;
        DataNascimento = dataNascimento;
        Contato = contato;
    }
    public CreateColaboradorRequest(string nome, string documento, DateTime dataNascimento, ContatoRequest contato)
    {
        Nome = nome; 
        Documento = documento;
        DataNascimento = dataNascimento;
        Contato = contato;
    }
}

public class UpdateColaboradorRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;
    public ContatoRequest Contato { get; set; } = null!;

    public UpdateColaboradorRequest() { }

    public UpdateColaboradorRequest(Guid id, string nome, ContatoRequest contato)
    {
        Id = id;
        Nome = nome;
        Contato = contato;
    }
}

public class GetAllColaboradoresRequest : PagedRequest
{
    public GetAllColaboradoresRequest() { }

    public GetAllColaboradoresRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false) : base(pageSize, pageNumber, query, returnAll)
    {

    }
}

public class GetColaboradorByIdRequest : Request
{
    public Guid Id { get; set; }

    public GetColaboradorByIdRequest() { }

    public GetColaboradorByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteColaboradorRequest : Request
{
    public Guid Id { get; set; }

    public DeleteColaboradorRequest() { }

    public DeleteColaboradorRequest(Guid id)
    {
        Id = id;
    }
}
