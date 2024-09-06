using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Requests.Shared;
public class EnderecoResponse
{
    [DisplayName("Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [DisplayName("Estado")]
    public string Estado { get; set; } = string.Empty;

    [DisplayName("CEP")]
    public string CEP { get; set; } = string.Empty;

    [DisplayName("Logradouro")]
    public string Logradouro { get; set; } = string.Empty;

    [DisplayName("Bairro")]
    public string Bairro { get; set; } = string.Empty;

    [DisplayName("Número")]
    public int Numero { get; set; }

    [DisplayName("Complemento")]
    public string? Complemento { get; set; }

    [DisplayName("Referência")]
    public string? Referencia { get; set; }

    public EnderecoResponse() { }

    public EnderecoResponse(string cidade, string estado, string cep, string logradouro, string bairro, int numero, string? complemento, string? referencia)
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
