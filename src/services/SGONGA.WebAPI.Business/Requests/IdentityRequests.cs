using SGONGA.WebAPI.Business.Responses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateUserRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    [DisplayName("Senha")]
    public string Senha { get; set; } = string.Empty;

    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    [DisplayName("Confirmar Senha")]
    public string ConfirmarSenha { get; set; } = string.Empty;

    public CreateUserRequest() { }

    public CreateUserRequest(Guid id, string email, string senha, string confirmarSenha)
    {
        Id = id;
        Email = email;
        Senha = senha;
        ConfirmarSenha = confirmarSenha;
    }
}

public class LoginUserRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    [DisplayName("Senha")]
    public string Senha { get; set; } = string.Empty;

    public LoginUserRequest() { }

    public LoginUserRequest(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}

public class UpdateUserEmailRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
    [DisplayName("Novo E-mail")]
    public string NovoEmail { get; set; } = string.Empty;

    public UpdateUserEmailRequest() { }

    public UpdateUserEmailRequest(Guid id, string novoEmail)
    {
        Id = id;
        NovoEmail = novoEmail;
    }
}

public class UpdateUserPasswordRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [DisplayName("Senha Antiga")]
    public string SenhaAntiga { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [DisplayName("Nova Senha")]
    public string NovaSenha { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
    [DisplayName("Confirmar Nova Senha")]
    public string ConfirmarNovaSenha { get; set; } = string.Empty;

    public UpdateUserPasswordRequest() { }

    public UpdateUserPasswordRequest(Guid id, string senhaAntiga, string novaSenha, string confirmarNovaSenha)
    {
        Id = id;
        SenhaAntiga = senhaAntiga;
        NovaSenha = novaSenha;
        ConfirmarNovaSenha = confirmarNovaSenha;
    }
}

public class AddOrUpdateUserClaimRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public UserClaim NewClaim { get; set; } = null!;

    public AddOrUpdateUserClaimRequest() { }

    public AddOrUpdateUserClaimRequest(Guid id, UserClaim newClaim)
    {
        Id = id;
        NewClaim = newClaim;
    }
}

public class GetUserByIdRequest : Request
{
    public Guid Id { get; set; }

    public GetUserByIdRequest() { }

    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteUserRequest : Request
{
    public Guid Id { get; set; }

    public DeleteUserRequest() { }

    public DeleteUserRequest(Guid id)
    {
        Id = id;
    }
}

public class GetAllUsersRequest : PagedRequest
{
    public GetAllUsersRequest() { }

    public GetAllUsersRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false) : base(pageSize, pageNumber, query, returnAll)
    {

    }
}