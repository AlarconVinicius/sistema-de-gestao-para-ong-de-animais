using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Errors;

namespace SGONGA.WebAPI.Business.Tests.Animals.Errors;

[Trait("Animals", "Errors")]
public class AnimalErrorsTests
{
    [Fact]
    public void AnimalNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        var animalId = Guid.NewGuid();

        // Act
        var result = AnimalErrors.AnimalNotFound(animalId);

        // Assert
        Assert.Equal("ANIMAL_NOT_FOUND", result.Code);
        Assert.Equal($"The animal with Id = '{animalId}' was not found.", result.Message);
        Assert.Equal(ErrorType.NotFound, result.Type);
    }

    [Fact]
    public void NoAnimalsFound_ShouldReturnNotFoundError()
    {
        // Act
        var result = AnimalErrors.NoAnimalsFound;

        // Assert
        Assert.Equal("NO_ANIMALS_FOUND", result.Code);
        Assert.Equal("No animals were found.", result.Message);
        Assert.Equal(ErrorType.NotFound, result.Type);
    }
}
