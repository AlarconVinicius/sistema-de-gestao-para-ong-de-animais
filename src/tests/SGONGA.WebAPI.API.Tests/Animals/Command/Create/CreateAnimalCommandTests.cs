using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Mocks;
using static SGONGA.WebAPI.API.Animals.Commands.Create.CreateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Create;

[Trait("Animal", "Commands")]
public class CreateAnimalCommandTests
{
    [Fact(DisplayName = "Constructor initialze all properties when called with valid parameters")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new CreateAnimalCommand(
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

    [Fact(DisplayName = "Constructor should initialize with default ChavePix when not provided")]
    public void Constructor_Should_InitializeWithDefaultChavePix_WhenNotProvided()
    {
        // Arrange
        var expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        var expectedChavePix = string.Empty;

        // Act
        var actualCommand = new CreateAnimalCommand(
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
            expectedCommand.Foto
        );

        // Assert
        actualCommand.ChavePix.Should().Be(expectedChavePix);
    }

    [Fact(DisplayName = "IsValid should return success when command is valid")]
    public void IsValid_Should_ReturnSuccess_WhenCommandIsValid()
    {
        // Arrange
        var command = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var result = command.IsValid();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "IsValid should return validation errors when command is invalid")]
    public void IsValid_Should_ReturnValidationFailures_WhenCommandIsInvalid()
    {
        // Arrange
        var command = AnimalDataFaker.GenerateInvalidCreateAnimalCommand();

        // Act
        var result = command.IsValid();

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(9);

        result.Errors.Should().BeEquivalentTo(new List<Error>
        {
            Error.Validation(CreateAnimalCommandValidator.NameRequired),
            Error.Validation(CreateAnimalCommandValidator.SpeciesRequired),
            Error.Validation(CreateAnimalCommandValidator.BreedRequired),
            Error.Validation(CreateAnimalCommandValidator.ColorRequired),
            Error.Validation(CreateAnimalCommandValidator.SizeRequired),
            Error.Validation(CreateAnimalCommandValidator.AgeRequired),
            Error.Validation(CreateAnimalCommandValidator.DescriptionRequired),
            Error.Validation(CreateAnimalCommandValidator.ObservationRequired),
            Error.Validation(CreateAnimalCommandValidator.PhotoRequired)
        });
    }
}
