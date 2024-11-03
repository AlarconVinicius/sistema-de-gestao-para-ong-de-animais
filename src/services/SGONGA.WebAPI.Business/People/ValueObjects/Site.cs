using SGONGA.WebAPI.Business.Abstractions;
using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.People.ValueObjects;
public sealed record Site : ValueObject
{
    #region Constants
    public const short MaxLength = 200;
    public const short MinLength = 10;
    public static readonly Regex SiteRegex = new(
        @"^(?:https?:\/\/)?(?:www\.)?(?:[a-z0-9]+(?:\.[a-z0-9]+)*\.)?[a-z0-9]+\.[a-z]{2,}(?:\/[\w\-\.\/]*)*$",
        RegexOptions.Compiled
    );
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
        if (!SiteRegex.IsMatch(url))
        {
            throw new ArgumentException("URL inválida.");
        }
    }
    #endregion
}
