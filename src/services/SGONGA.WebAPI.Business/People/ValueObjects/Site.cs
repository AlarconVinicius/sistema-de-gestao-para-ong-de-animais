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
        if(!IsValidUrl(url))
            throw new PersonValidationException(Error.Validation("URL inválida.")); 
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
