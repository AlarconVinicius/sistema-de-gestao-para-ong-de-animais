using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("People", "ValueObjects")]
public sealed class EmailTests
{
    [Fact]
    public void Constructor_Should_Initialize_WhenCalledWithValidEmails()
    {
        // Arrange
        string validEmail = "email@email.com";
        // Act
        Email email = validEmail;

        // Assert
        Assert.Equal(validEmail, email.Address);
        Assert.True(email.Address.Length < Email.MaxLength);
        Assert.True(email.Address.Length > Email.MinLength);
    }

    [Theory]
    [InlineData("email@")]
    [InlineData("email@domain")]
    [InlineData("email@domain.c")]
    [InlineData("email!@domain.com")]
    [InlineData("email@domain#.com")]
    [InlineData("e..mail@domain.com")]
    [InlineData("email@com")]
    [InlineData("emal@(domain.com)")]
    public void Constructor_Should_ThrowException_WhenCalledWithVInvalidEmails(string invalidEmail)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Email email = invalidEmail;
        });

        Assert.Contains(exception.Errors, e => e.Message == "Email inválido");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("emai")]
    [InlineData("email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email email")]
    public void Constructor_Should_ThrowException_WhenCalledWithInvalidLength(string invalidEmail)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Email email = invalidEmail;
        });

        Assert.Contains(exception.Errors, e => e.Message == $"E-mail deve conter entre {Email.MinLength} e {Email.MaxLength} caracteres.");
    }
}
