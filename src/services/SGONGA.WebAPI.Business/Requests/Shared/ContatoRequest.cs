using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Requests.Shared;
public class ContatoRequest
{
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

    public ContatoRequest() { }

    public ContatoRequest(string telefone, string email)
    {
        Telefone = telefone;
        Email = email;
    }
}
