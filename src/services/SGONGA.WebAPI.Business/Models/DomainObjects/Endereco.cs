using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.Models.DomainObjects;

public class Endereco
{
    public const int ComprimentoMaxRua = 100;
    public const int ComprimentoMaxCidade = 50;
    public const int ComprimentoMaxEstado = 50;
    public const int ComprimentoMaxCEP = 10;

    public string Rua { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;
    public string CEP { get; private set; } = string.Empty;
    public string Complemento { get; private set; } = string.Empty;

    protected Endereco() { }

    public Endereco(string rua, string cidade, string estado, string cep, string complemento = "")
    {
        if (string.IsNullOrEmpty(rua) ||
                string.IsNullOrEmpty(cidade) ||
                string.IsNullOrEmpty(estado) ||
                !ValidarCEP(cep))
        {
            throw new ArgumentException("Dados de endereço inválidos.");
        }

        Rua = rua;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Complemento = complemento;
    }

    public static bool ValidarCEP(string cep)
    {
        var regexCEP = new Regex(@"^\d{5}-?\d{3}$");
        return regexCEP.IsMatch(cep);
    }
}