using SGONGA.Core.Utils;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public sealed record Email : ValueObject
{
    #region Constants
    public const short MaxLength = 254;
    public const short MinLength = 5;
    #endregion

    #region Constructors
    private Email(string address)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(address) || address.Length > MaxLength || address.Length < MinLength)
            errors.Add($"E-mail deve conter entre {MinLength} e {MaxLength} caracteres.");

        if (!IsValidEmail(address))
            errors.Add("Email inválido");

        if (errors.Count > 0)
            throw new PersonValidationException(errors.Select(Error.Validation).ToArray());

        Address = address.ToLower();
    }
    #endregion

    #region Properties
    public string Address { get; }
    #endregion

    #region Operators
    public static implicit operator string(Email email) => email.Address;
    public static implicit operator Email(string url) => new(url);
    #endregion
    #region Methods
    private static bool IsValidEmail(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;
        return RegexUtils.EmailRegex.IsMatch(address);
    }
    #endregion
}
