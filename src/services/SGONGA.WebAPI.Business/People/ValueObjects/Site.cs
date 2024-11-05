using SGONGA.Core.Utils;
using SGONGA.WebAPI.Business.Abstractions;

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
        ValidateUrl(url);
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
    private static void ValidateUrl(string url)
    {
        if (!RegexUtils.SiteRegex.IsMatch(url))
        {
            throw new ArgumentException("URL inválida.");
        }
    }
    #endregion
}
