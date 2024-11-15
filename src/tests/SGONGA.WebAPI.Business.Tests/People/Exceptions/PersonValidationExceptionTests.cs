using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.People.Exceptions;

[Trait("People", "Exceptions")]
public sealed class PersonValidationExceptionTests
{
    [Fact]
    public async Task PersonValidationException_Should_Contain_Single_Error()
    {
        // Arrange
        var error = Error.Validation("Invalid CPF format");

        // Act
        var exception = await Assert.ThrowsAsync<PersonValidationException>(() =>
            throw new PersonValidationException(error));

        // Assert
        Assert.Equal("FALHA_AO_VALIDAR_PESSOA", exception.Message);
        Assert.Single(exception.Errors);
        Assert.Equal("Invalid CPF format", exception.Errors[0].Message);
        Assert.Equal("VALIDATION_ERROR", exception.Errors[0].Code);
        Assert.Equal(ErrorType.Validation, exception.Errors[0].Type);
    }

    [Fact]
    public async Task PersonValidationException_Should_Contain_Multiple_Errors()
    {
        // Arrange
        var errors = new[]
        {
        Error.Validation("Invalid CPF format"),
        Error.Validation("Invalid name format")
    };

        // Act
        var exception = await Assert.ThrowsAsync<PersonValidationException>(() =>
            throw new PersonValidationException(errors));

        // Assert
        Assert.Equal("FALHA_AO_VALIDAR_PESSOA", exception.Message);
        Assert.Equal(2, exception.Errors.Length);
        Assert.Equal("VALIDATION_ERROR", exception.Errors[0].Code);
        Assert.Equal("Invalid CPF format", exception.Errors[0].Message);
        Assert.Equal(ErrorType.Validation, exception.Errors[0].Type);

        Assert.Equal("VALIDATION_ERROR", exception.Errors[1].Code);
        Assert.Equal("Invalid name format", exception.Errors[1].Message);
        Assert.Equal(ErrorType.Validation, exception.Errors[1].Type);
    }
}
