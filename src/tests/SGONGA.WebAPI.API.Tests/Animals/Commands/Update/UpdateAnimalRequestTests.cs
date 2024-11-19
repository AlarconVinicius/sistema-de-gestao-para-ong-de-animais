using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.API.Tests.Animals.Commands.Update;

[Trait("Animals", "Requests")]
public class UpdateAnimalRequestTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new UpdateAnimalRequest(
            expectedCommand.Name,
            expectedCommand.Species,
            expectedCommand.Breed,
            expectedCommand.Gender,
            expectedCommand.Neutered,
            expectedCommand.Color,
            expectedCommand.Size,
            expectedCommand.Age,
            expectedCommand.Description,
            expectedCommand.Note,
            expectedCommand.Photo,
            expectedCommand.PixKey
        );

        // Assert
        actualCommand.Should().BeEquivalentTo(expectedCommand, options =>
            options.Excluding(a => a.ValidationResult));
    }
}
