using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public sealed record Document : ValueObject
{
    #region Constants
    public const short CpfLength = 11;
    public const short CnpjLength = 14;
    #endregion

    #region Constructors
    private Document(string number)
    {
        var cleanNumber = number.OnlyNumbers();
        if (!Validate(cleanNumber))
        {
            throw new DomainException($"Documento inválido: {number}");
        }
        Number = cleanNumber;
    }
    #endregion

    #region Properties
    public string Number { get; }
    public bool IsCpf => Number.Length == CpfLength;
    public bool IsCnpj => Number.Length == CnpjLength;
    #endregion

    #region Operators
    public static implicit operator string(Document document) => document.Number;
    public static implicit operator Document(string number) => new(number);
    #endregion

    #region Validators
    public static bool Validate(string document)
    {
        var cleanDocument = document.OnlyNumbers();
        return cleanDocument.Length == CpfLength
            ? ValidateCpf(cleanDocument)
            : ValidateCnpj(cleanDocument);
    }

    private static bool ValidateCpf(string cpf)
    {
        if (cpf.Length != CpfLength)
            return false;

        var areAllDigitsEqual = true;
        for (var i = 1; i < CpfLength && areAllDigitsEqual; i++)
            if (cpf[i] != cpf[0])
                areAllDigitsEqual = false;

        if (areAllDigitsEqual || cpf == "12345678909")
            return false;

        var numbers = new int[CpfLength];
        for (var i = 0; i < CpfLength; i++)
            numbers[i] = int.Parse(cpf[i].ToString());

        var sum = 0;
        for (var i = 0; i < CpfLength - 2; i++)
            sum += (CpfLength - 1 - i) * numbers[i];

        var result = sum % 11;
        if (result == 1 || result == 0)
        {
            if (numbers[9] != 0)
                return false;
        }
        else if (numbers[9] != 11 - result)
            return false;

        sum = 0;
        for (var i = 0; i < CpfLength - 1; i++)
            sum += (CpfLength - i) * numbers[i];

        result = sum % 11;
        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
                return false;
        }
        else if (numbers[10] != 11 - result)
            return false;

        return true;
    }

    private static bool ValidateCnpj(string cnpj)
    {
        if (cnpj.Length != CnpjLength)
            return false;

        var areAllDigitsEqual = true;
        for (var i = 1; i < CnpjLength && areAllDigitsEqual; i++)
            if (cnpj[i] != cnpj[0])
                areAllDigitsEqual = false;

        if (areAllDigitsEqual)
            return false;

        var numbers = new int[CnpjLength];
        for (var i = 0; i < CnpjLength; i++)
            numbers[i] = int.Parse(cnpj[i].ToString());

        var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var sum = 0;

        for (var i = 0; i < 12; i++)
            sum += numbers[i] * multiplier1[i];

        var result = sum % 11;
        var digit1 = result < 2 ? 0 : 11 - result;

        if (numbers[12] != digit1)
            return false;

        var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        sum = 0;

        for (var i = 0; i < 13; i++)
            sum += numbers[i] * multiplier2[i];

        result = sum % 11;
        var digit2 = result < 2 ? 0 : 11 - result;

        return numbers[13] == digit2;
    }
    #endregion

    #region Formatting
    public string Format()
    {
        if (IsCpf)
            return Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

        return Convert.ToUInt64(Number).ToString(@"00\.000\.000\/0000\-00");
    }
    public override string ToString() => Format();
    #endregion
}