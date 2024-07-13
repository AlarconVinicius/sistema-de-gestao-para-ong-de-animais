using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class UserResponse
{
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    public UserResponse() { }

    public UserResponse(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}

public class LoginUserResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; } = null!;

    public LoginUserResponse() { }

    public LoginUserResponse(string accessToken, double expiresIn, UserToken userToken)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        UserToken = userToken;
    }
}
public class UserToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<UserClaim> Claims { get; set; } = new List<UserClaim>();

    public UserToken() { }

    public UserToken(string id, string email, IEnumerable<UserClaim> claims)
    {
        Id = id;
        Email = email;
        Claims = claims;
    }
}
public class UserClaim
{
    public string Value { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public UserClaim() { }

    public UserClaim(string type, string value)
    {
        Value = value;
        Type = type;
    }
}