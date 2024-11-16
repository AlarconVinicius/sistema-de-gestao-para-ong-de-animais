using SGONGA.WebAPI.Business.Animals.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.Animals.Exceptions;

[Trait("Animals", "Exceptions")]
public class AnimalNotFoundExceptionTests
{
    [Fact]
    public async Task AnimalNotFoundException_Should_Contain_Correct_Message()
    {
        // Arrange
        var animalId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AnimalNotFoundException>(() =>
            throw new AnimalNotFoundException(animalId));

        // Assert
        Assert.Equal($"The animal with Id = '{animalId.ToString()}' was not found.", exception.Message);
    }
}
