using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("Person", "ValueObjects - PhoneNumber")]
public class PhoneNumberTests
{
    [Theory]
    [InlineData("123456789")]
    [InlineData("12345-6789")]
    [InlineData("11987654321")]
    [InlineData("(11) 98765-4321")]
    public void Constructor_ShouldInitialize_WhenCalledWithValidPhoneNumbers(string validPhoneNumber)
    {
        // Arrange & Act
        PhoneNumber phoneNumber = validPhoneNumber;

        // Assert
        Assert.Equal(validPhoneNumber.OnlyNumbers(), phoneNumber.Number);
        Assert.True(phoneNumber.Number.Length == PhoneNumber.LengthWithDDD || phoneNumber.Number.Length == PhoneNumber.LengthWithoutDDD);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123456789012")]
    [InlineData("abcdefghijk")]
    [InlineData("112345678901")]
    [InlineData("11234567")]
    public void Constructor_ShouldThrowException_WhenCalledWithInvalidPhoneNumbers(string invalidPhoneNumber)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            PhoneNumber phoneNumber = invalidPhoneNumber;
        });

        Assert.Contains(exception.Errors, e => e.Message == "Número de telefone inválido. Deve conter exatamente 9 ou 11 dígitos.");
    }
}
