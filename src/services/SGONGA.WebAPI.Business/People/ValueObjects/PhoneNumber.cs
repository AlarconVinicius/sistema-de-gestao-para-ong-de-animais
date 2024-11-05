using SGONGA.Core.Extensions;
using SGONGA.Core.Utils;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public class PhoneNumber
{
    #region Constants
    public const short LengthWithDDD = 11;
    public const short LengthWithoutDDD = 9;
    #endregion

    #region Constructors
    public PhoneNumber(string number)
    {
        var cleanNumber = number.OnlyNumbers();
        ValidatePhoneNumber(cleanNumber);
        Number = cleanNumber;
    }
    #endregion

    #region Properties
    public string Number { get; }
    #endregion

    #region Operators
    public static implicit operator string(PhoneNumber number) => number.Number;
    public static implicit operator PhoneNumber(string number) => new(number);
    #endregion
    #region Methods
    public static void ValidatePhoneNumber(string number)
    {
        if (!RegexUtils.PhoneRegex.IsMatch(number) ||
            (number.Length != LengthWithoutDDD && number.Length != LengthWithDDD))
        {
            throw new ArgumentException("Número de telefone inválido. Deve conter exatamente 9 ou 11 dígitos.");
        }
    }
    #endregion
}
