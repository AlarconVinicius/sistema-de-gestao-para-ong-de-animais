using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.Animals.Exceptions;

[Trait("Animals", "Exceptions")]
public class AnimalValidationExceptionTests
{
    [Fact]
    public async Task AnimalValidationException_Should_Contain_Single_Error()
    {
        // Arrange
        var error = Error.Validation("Invalid name.");

        // Act
        var exception = await Assert.ThrowsAsync<AnimalValidationException>(() =>
            throw new AnimalValidationException(error));

        // Assert
        Assert.Equal("ANIMAL_VALIDATION_FAILED", exception.Message);
        Assert.Single(exception.Errors);
        Assert.Equal("Invalid name.", exception.Errors[0].Message);
        Assert.Equal("VALIDATION_ERROR", exception.Errors[0].Code);
        Assert.Equal(ErrorType.Validation, exception.Errors[0].Type);
    }

    [Fact]
    public async Task AnimalValidationException_Should_Contain_Multiple_Errors()
    {
        // Arrange
        var errors = new[]
        {
        Error.Validation("Invalid name."),
        Error.Validation("Invalid breed.")
    };

        // Act
        var exception = await Assert.ThrowsAsync<AnimalValidationException>(() =>
            throw new AnimalValidationException(errors));

        // Assert
        Assert.Equal("ANIMAL_VALIDATION_FAILED", exception.Message);
        Assert.Equal(2, exception.Errors.Length);
        Assert.Equal("VALIDATION_ERROR", exception.Errors[0].Code);
        Assert.Equal("Invalid name.", exception.Errors[0].Message);
        Assert.Equal(ErrorType.Validation, exception.Errors[0].Type);

        Assert.Equal("VALIDATION_ERROR", exception.Errors[1].Code);
        Assert.Equal("Invalid breed.", exception.Errors[1].Message);
        Assert.Equal(ErrorType.Validation, exception.Errors[1].Type);
    }
}
