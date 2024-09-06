using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests.Shared;
public class EnderecoRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(2, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("CEP")]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Logradouro")]
    public string Logradouro { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Bairro")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Número")]
    public int Numero { get; set; }

    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Complemento")]
    public string? Complemento { get; set; }

    [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Referência")]
    public string? Referencia { get; set; }

    public EnderecoRequest() { }

    public EnderecoRequest(string cidade, string estado, string cep, string logradouro, string bairro, int numero, string? complemento, string? referencia)
    {
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Referencia = referencia;
    }
}
