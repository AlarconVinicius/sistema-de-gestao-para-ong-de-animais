using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("Person", "ValueObjects - Document")]
public class DocumentTests
{
    #region CPF Tests
    [Fact]
    public void Constructor_ShouldInitialize_WhenCalledWithValidCPF()
    {
        // Arrange
        string validCpf = "62654441075";
        // Act
        Document document = validCpf;

        // Assert
        Assert.Equal(validCpf, document.Number);
        Assert.True(document.IsCpf);
        Assert.True(document.Number.Length == Document.CpfLength);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenCalledWithInvalidCPF()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Document document = "62654441065";
        });

        Assert.Contains(exception.Errors, e => e.Message == "Documento inválido: 62654441065");
    }

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
    [Fact]
    public void Constructor_ShouldInitialize_WhenCalledWithValidCNPJ()
    {
        // Arrange
        string validCnpj = "11444777000161";
        // Act
        Document document = validCnpj;

        // Assert
        Assert.Equal(validCnpj, document.Number);
        Assert.True(document.IsCnpj);
        Assert.True(document.Number.Length == Document.CnpjLength);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenCalledWithInvalidCNPJ()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Document document = "11444777000171";
        });

        Assert.Contains(exception.Errors, e => e.Message == "Documento inválido: 11444777000171");
    }

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

    #region Document Formatting Tests
    [Theory]
    [InlineData("62654441075", "626.544.410-75")] // Valid CPF
    [InlineData("82278834002", "822.788.340-02")] // Another valid CPF
    [InlineData("29828241080", "298.282.410-80")] // Valid CPF with formatting
    [InlineData("99311650064", "993.116.500-64")] // Another valid CPF with formatting
    [InlineData("11444777000161", "11.444.777/0001-61")] // Valid CNPJ
    [InlineData("62823257000109", "62.823.257/0001-09")] // Valid CNPJ
    [InlineData("45997418000153", "45.997.418/0001-53")] // Valid CNPJ
    public void DocumentFormat_Should_Return_Correct_Format(string documentNumber, string expectedFormat)
    {
        // Arrange
        Document document = documentNumber;

        // Act
        var formatted = document.Format();

        // Assert
        Assert.Equal(expectedFormat, formatted);
    }

    [Theory]
    [InlineData("62654441075", "626.544.410-75")] // Valid CPF
    [InlineData("82278834002", "822.788.340-02")] // Another valid CPF
    [InlineData("11444777000161", "11.444.777/0001-61")] // Valid CNPJ
    [InlineData("62823257000109", "62.823.257/0001-09")] // Valid CNPJ
    public void DocumentToString_Should_Return_Correct_Format(string documentNumber, string expectedFormat)
    {
        // Arrange
        Document document = documentNumber;

        // Act
        var formatted = document.ToString();

        // Assert
        Assert.Equal(expectedFormat, formatted);
    }
    #endregion
}
