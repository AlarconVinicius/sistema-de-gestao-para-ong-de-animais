using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("Person", "ValueObjects - Document")]
public class DocumentTests
{
    #region CPF Tests
    [Theory]
    [InlineData("12345678900")] // Invalid CPF
    [InlineData("11144477731")] // Invalid CPF with incorrect check digits
    [InlineData("11111111111")] // CPF with all identical digits (11111111111)
    [InlineData("00000000000")] // CPF with all identical digits (00000000000)
    [InlineData("123456789")] // CPF too short
    [InlineData("1234567890912")] // CPF too long
    [InlineData("123.456.789-00")] // Invalid CPF with formatting
    [InlineData("111.444.777-31")] // Another invalid CPF with formatting
    [InlineData("1234567890A")] // CPF with a letter at the end
    [InlineData("A2345678909")] // CPF with a letter at the start
    [InlineData("12345678009")] // Invalid CPF: fails first check digit (covering the "numbers[9] != (result < 2 ? 0 : 11 - result)" check)
    [InlineData("")] // Empty CPF
    public void ValidateCpf_ShouldReturnFalse_ForInvalidCpf(string cpf)
    {
        // Act
        bool result = Document.ValidateCpf(cpf);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("62654441075")] // Valid CPF
    [InlineData("82278834002")] // Another valid CPF
    [InlineData("298.282.410-80")] // Valid CPF with formatting
    [InlineData("993.116.500-64")] // Another valid CPF with formatting
    public void ValidateCpf_ShouldReturnTrue_ForValidCpf(string cpf)
    {
        // Act
        bool result = Document.ValidateCpf(cpf);

        // Assert
        Assert.True(result);
    }
    #endregion

    #region CNPJ Tests
    [Theory]
    [InlineData("11111111111111")] // CNPJ with all digits the same
    [InlineData("00000000000000")] // CNPJ with zeros
    [InlineData("1234567890123")] // CNPJ with incorrect length (13 digits)
    [InlineData("123456789012345")] // CNPJ with incorrect length (15 digits)
    [InlineData("12345678901234")] // CNPJ with invalid check digits
    [InlineData("11444777000171")] // Invalid CNPJ: fails first check digit(original: 11444777000161)
    [InlineData("")] // Empty CNPJ
    public void ValidateCnpj_ShouldReturnFalse_ForInvalidCnpj(string cnpj)
    {
        // Act
        bool result = Document.ValidateCnpj(cnpj);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("11444777000161")] // Valid CNPJ
    [InlineData("62823257000109")] // Valid CNPJ
    [InlineData("45997418000153")] // Valid CNPJ
    public void ValidateCnpj_ShouldReturnTrue_ForValidCnpj(string cnpj)
    {
        // Act
        bool result = Document.ValidateCnpj(cnpj);

        // Assert
        Assert.True(result);
    }
    #endregion
}
