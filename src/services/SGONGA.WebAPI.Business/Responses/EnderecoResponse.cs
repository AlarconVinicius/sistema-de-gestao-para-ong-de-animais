using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class EnderecoResponse : Response
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Rua")]
    public string Rua { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("CEP")]
    public string Cep { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Complemento")]
    public string Complemento { get; set; } = string.Empty;

    public EnderecoResponse() { }

    public EnderecoResponse(string rua, string cidade, string estado, string cep, string complemento = "")
    {
        Rua = rua;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
        Complemento = complemento;
    }
}