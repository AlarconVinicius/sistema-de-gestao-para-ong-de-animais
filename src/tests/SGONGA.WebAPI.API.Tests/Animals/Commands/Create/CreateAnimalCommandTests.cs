using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Mocks;
using static SGONGA.WebAPI.API.Animals.Commands.Create.CreateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Commands.Create;

[Trait("Animals", "Commands")]
public class CreateAnimalCommandTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new CreateAnimalCommand(
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
        actualCommand.Should().BeEquivalentTo(expectedCommand);
    }

    [Fact(DisplayName = "Constructor Initialize With Default ChavePix")]
    public void Constructor_Should_InitializeWithDefaultChavePix_WhenNotProvided()
    {
        // Arrange
        var expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();

        // Act
        var actualCommand = new CreateAnimalCommand(
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
            null
        );

        // Assert
        actualCommand.PixKey.Should().Be(null);
    }

    [Fact(DisplayName = "IsValid Return Success")]
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

    [Fact(DisplayName = "IsValid Return Validation Failures")]
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
            Error.Validation(CreateAnimalCommandValidator.NoteMaxLength),
            Error.Validation(CreateAnimalCommandValidator.PhotoRequired)
        });
    }
}
