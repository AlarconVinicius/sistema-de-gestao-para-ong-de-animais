using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public class PhoneNumber
{
    #region Constants
    public const int MaxLength = 15;
    public const int MinLength = 9;
    public static readonly Regex PhoneRegex = new(
        @"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})",
        RegexOptions.Compiled
    );
    #endregion

    #region Constructors
    public PhoneNumber(string number)
    {
        ValidatePhoneNumber(number);
        Number = number;
    }
    #endregion

    #region Properties
    public string Number { get; private set; } = string.Empty;
    #endregion

    #region Operators
    public static implicit operator string(PhoneNumber number) => number.Number;
    public static implicit operator PhoneNumber(string number) => new(number);
    #endregion
    #region Methods
    public static void ValidatePhoneNumber(string number)
    {
        if (!PhoneRegex.IsMatch(number))
        {
            throw new ArgumentException("Número de telefone inválido.");
        }
    }
    #endregion
}
