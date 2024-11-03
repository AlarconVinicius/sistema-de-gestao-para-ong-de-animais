using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public class Email
{
    #region Constants
    public const int ComprimentoMaxEndereco = 254;
    public const int ComprimentoMinEndereco = 5;
    public static readonly Regex EmailRegex = new(
        @"^(?("")("".+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]\.)+[a-z]{2,6}))$",
        RegexOptions.Compiled
    );
    #endregion

    #region Constructors
    public Email(string address)
    {
        ValidateEmail(address);
        Address = address;
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
        if (!EmailRegex.IsMatch(address))
        {
            throw new ArgumentException("Email inválido");
        }
    }
    #endregion
}
