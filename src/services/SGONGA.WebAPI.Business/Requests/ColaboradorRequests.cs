using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateColaboradorRequest : Request
{
    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(254, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    public CreateColaboradorRequest() { }

    public CreateColaboradorRequest(string email)
    {
        Email = email;
    }
}

public class UpdateColaboradorRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(254, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    public UpdateColaboradorRequest() { }

    public UpdateColaboradorRequest(Guid id, string email)
    {
        Id = id;
        Email = email;
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
