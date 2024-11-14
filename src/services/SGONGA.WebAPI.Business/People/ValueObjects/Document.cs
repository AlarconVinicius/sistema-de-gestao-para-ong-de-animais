using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Exceptions;

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
            throw new PersonValidationException(Error.Validation($"Documento inválido: {number}"));
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

    public static bool ValidateCpf(string cpf)
    {
        cpf = cpf.OnlyNumbers();

        if (cpf.Length != CpfLength || cpf.Distinct().Count() == 1 || cpf == "12345678909")
            return false;

        int[] numbers = cpf.Select(c => c - '0').ToArray();
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += numbers[i] * (10 - i);

        int result = sum % 11;
        if (numbers[9] != (result < 2 ? 0 : 11 - result))
            return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += numbers[i] * (11 - i);

        result = sum % CpfLength;
        return numbers[10] == (result < 2 ? 0 : 11 - result);
    }

    public static bool ValidateCnpj(string cnpj)
    {
        cnpj = cnpj.OnlyNumbers();

        if (cnpj.Length != CnpjLength)
            return false;

        if (cnpj.Distinct().Count() == 1)
            return false;

        int[] numbers = cnpj.Select(c => c - '0').ToArray();
        int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int sum1 = numbers.Take(12).Select((num, i) => num * multiplier1[i]).Sum();
        int digit1 = sum1 % 11 < 2 ? 0 : 11 - (sum1 % 11);

        if (numbers[12] != digit1)
            return false;

        int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int sum2 = numbers.Take(13).Select((num, i) => num * multiplier2[i]).Sum();
        int digit2 = sum2 % 11 < 2 ? 0 : 11 - (sum2 % 11);

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