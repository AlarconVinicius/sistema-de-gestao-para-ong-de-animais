using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.People.ValueObjects;
public sealed record Site : ValueObject
{
    #region Constants
    public const short MaxLength = 200;
    public const short MinLength = 10;
    #endregion

    #region Constructors
    private Site(string url)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(url) || url.Length > MaxLength || url.Length < MinLength)
            errors.Add($"URL deve conter entre {MinLength} e {MaxLength} caracteres.");

        if (!IsValidUrl(url))
            errors.Add("URL inválida. O formato esperado deve começar com 'http://' ou 'https://', seguido de um domínio válido, como 'https://site.com' ou 'http://site.com'.");

        if (errors.Count > 0)
            throw new PersonValidationException(errors.Select(Error.Validation).ToArray());

        Url = url;
    }
    #endregion

    #region Properties
    public string Url { get; }
    #endregion

    #region Operators
    public static implicit operator string(Site site) => site.Url;
    public static implicit operator Site(string url) => new(url);
    #endregion

    #region Methods
    public static bool IsValidUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
        (uri.Scheme != "http" && uri.Scheme != "https"))
        {
            return false;
        }
        return true;
    }
    #endregion
}
