using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Delete;

namespace SGONGA.WebAPI.API.Tests.Animals.Commands.Delete;

[Trait("Animals", "Commands")]
public class DeleteAnimalCommandTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DeleteAnimalCommand expectedCommand = new(id);

        // Act
        var actualCommand = new DeleteAnimalCommand(id);

        // Assert
        actualCommand.Should().BeEquivalentTo(expectedCommand);
    }
}
