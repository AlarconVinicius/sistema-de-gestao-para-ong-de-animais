using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Requests.Shared;
public class ContatoResponse
{
    [DisplayName("Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    public ContatoResponse() { }

    public ContatoResponse(string telefone, string email)
    {
        Telefone = telefone;
        Email = email;
    }
}
