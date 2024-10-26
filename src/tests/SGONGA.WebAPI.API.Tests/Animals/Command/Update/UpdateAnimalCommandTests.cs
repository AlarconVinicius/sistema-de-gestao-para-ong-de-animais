using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Mocks;
using static SGONGA.WebAPI.API.Animals.Commands.Update.UpdateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Update;

[Trait("Animal", "Command - Update")]
public class UpdateAnimalCommandTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new UpdateAnimalCommand(
            Guid.NewGuid(),
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
        actualCommand.Should().BeEquivalentTo(expectedCommand);
    }

    [Fact(DisplayName = "IsValid Return Success")]
    public void IsValid_Should_ReturnSuccess_WhenCommandIsValid()
    {
        // Arrange
        var baseCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        var command = new UpdateAnimalCommand(
            Guid.NewGuid(),
            baseCommand.Nome,
            baseCommand.Especie,
            baseCommand.Raca,
            baseCommand.Sexo,
            baseCommand.Castrado,
            baseCommand.Cor,
            baseCommand.Porte,
            baseCommand.Idade,
            baseCommand.Descricao,
            baseCommand.Observacao,
            baseCommand.Foto,
            baseCommand.ChavePix
        );

        // Act
        var result = command.IsValid();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "IsValid Return Validation Failures")]
    public void IsValid_Should_ReturnValidationFailures_WhenCommandIsInvalid()
    {
        // Arrange
        var baseCommand = AnimalDataFaker.GenerateInvalidCreateAnimalCommand();
        var command = new UpdateAnimalCommand(
            Guid.NewGuid(),
            baseCommand.Nome,
            baseCommand.Especie,
            baseCommand.Raca,
            baseCommand.Sexo,
            baseCommand.Castrado,
            baseCommand.Cor,
            baseCommand.Porte,
            baseCommand.Idade,
            baseCommand.Descricao,
            baseCommand.Observacao,
            baseCommand.Foto,
            baseCommand.ChavePix
        );

        // Act
        var result = command.IsValid();

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(9);

        result.Errors.Should().BeEquivalentTo(new List<Error>
        {
            Error.Validation(UpdateAnimalCommandValidator.NameRequired),
            Error.Validation(UpdateAnimalCommandValidator.SpeciesRequired),
            Error.Validation(UpdateAnimalCommandValidator.BreedRequired),
            Error.Validation(UpdateAnimalCommandValidator.ColorRequired),
            Error.Validation(UpdateAnimalCommandValidator.SizeRequired),
            Error.Validation(UpdateAnimalCommandValidator.AgeRequired),
            Error.Validation(UpdateAnimalCommandValidator.DescriptionRequired),
            Error.Validation(UpdateAnimalCommandValidator.ObservationRequired),
            Error.Validation(UpdateAnimalCommandValidator.PhotoRequired)
        });
    }
}
