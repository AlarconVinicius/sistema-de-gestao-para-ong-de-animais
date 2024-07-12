using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.Models.DomainObjects;

public class Telefone
{
    public const int ComprimentoMaxNumero = 15;
    public const int ComprimentoMinNumero = 9;
    public string Numero { get; private set; } = string.Empty;

    protected Telefone() { }

    public Telefone(string numero)
    {
        if (!Validar(numero)) throw new DomainException("Número inválido");
        Numero = numero;
    }

    public static bool Validar(string telefone)
    {
        var regexTelefone = new Regex(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})");
        return regexTelefone.IsMatch(telefone);
    }
}
