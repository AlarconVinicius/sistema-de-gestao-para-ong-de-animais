using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Update;

[Trait("Animal", "Request - Update")]
public class UpdateAnimalRequestTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new UpdateAnimalRequest(
            expectedCommand.Nome,
            expectedCommand.Especie,
            expectedCommand.Raca,
            expectedCommand.Sexo,
            expectedCommand.Castrado,
            expectedCommand.Cor,
            expectedCommand.Porte,
            expectedCommand.Idade,
            expectedCommand.Descricao,
            expectedCommand.Observacao,
            expectedCommand.Foto,
            expectedCommand.ChavePix
        );

        // Assert
        actualCommand.Should().BeEquivalentTo(expectedCommand, options =>
            options.Excluding(a => a.ValidationResult));
    }
}
