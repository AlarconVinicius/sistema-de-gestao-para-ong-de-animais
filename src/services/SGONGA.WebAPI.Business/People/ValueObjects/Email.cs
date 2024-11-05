using SGONGA.Core.Utils;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public class Email
{
    #region Constants
    public const short MaxLength = 254;
    public const short MinLength = 5;
    #endregion

    #region Constructors
    public Email(string address)
    {
        ValidateEmail(address);
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
    private static void ValidateEmail(string address)
    {
        if (!RegexUtils.EmailRegex.IsMatch(address))
        {
            throw new ArgumentException("Email inválido");
        }
    }
    #endregion
}
