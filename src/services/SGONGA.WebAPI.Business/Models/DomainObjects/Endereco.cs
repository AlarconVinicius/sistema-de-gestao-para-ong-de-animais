using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.Models.DomainObjects;

public class Endereco
{
    public const int ComprimentoMaxLogradouro = 100;
    public const int ComprimentoMaxCidade = 50;
    public const int ComprimentoMaxEstado = 50;
    public const int ComprimentoMaxCEP = 10;

    public string Cidade { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;
    public string CEP { get; private set; } = string.Empty;
    public string Logradouro { get; private set; } = string.Empty;
    public string Bairro { get; private set; } = string.Empty;
    public int Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string? Referencia { get; private set; }

    protected Endereco() { }

    public Endereco(string cidade, string estado, string cep, string logradouro, string bairro, int numero, string? complemento, string? referencia)
    {
        if(string.IsNullOrEmpty(cidade) ||
            string.IsNullOrEmpty(estado) ||
            string.IsNullOrEmpty(logradouro) ||
            string.IsNullOrEmpty(bairro) ||
            !ValidarCEP(cep))
        {
            throw new ArgumentException("Dados de endereço inválidos.");
        }
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Logradouro = logradouro;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        Referencia = referencia;
    }

    public static bool ValidarCEP(string cep)
    {
        var regexCEP = new Regex(@"^\d{5}-?\d{3}$");
        return regexCEP.IsMatch(cep);
    }
}