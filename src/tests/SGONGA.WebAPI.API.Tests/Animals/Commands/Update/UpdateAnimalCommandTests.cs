using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Mocks;
using static SGONGA.WebAPI.API.Animals.Commands.Update.UpdateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Commands.Update;

[Trait("Animals", "Commands")]
public class UpdateAnimalCommandTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        CreateAnimalCommand expectedCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        Guid animalId = Guid.NewGuid();
        // Act
        var actualCommand = new UpdateAnimalCommand(
            animalId,
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
        actualCommand.Id.Should().Be(animalId);
        actualCommand.Should().BeEquivalentTo(expectedCommand);
    }

    [Fact(DisplayName = "IsValid Return Success")]
    public void IsValid_Should_ReturnSuccess_WhenCommandIsValid()
    {
        // Arrange
        var baseCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        var command = new UpdateAnimalCommand(
            Guid.NewGuid(),
            baseCommand.Name,
            baseCommand.Species,
            baseCommand.Breed,
            baseCommand.Gender,
            baseCommand.Neutered,
            baseCommand.Color,
            baseCommand.Size,
            baseCommand.Age,
            baseCommand.Description,
            baseCommand.Note,
            baseCommand.Photo,
            baseCommand.PixKey
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
            baseCommand.Name,
            baseCommand.Species,
            baseCommand.Breed,
            baseCommand.Gender,
            baseCommand.Neutered,
            baseCommand.Color,
            baseCommand.Size,
            baseCommand.Age,
            baseCommand.Description,
            baseCommand.Note,
            baseCommand.Photo,
            baseCommand.PixKey
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
            Error.Validation(UpdateAnimalCommandValidator.NoteMaxLength),
            Error.Validation(UpdateAnimalCommandValidator.PhotoRequired)
        });
    }
}
