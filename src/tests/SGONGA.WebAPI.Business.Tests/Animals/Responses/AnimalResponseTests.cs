using FluentAssertions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.Business.Tests.Animals.Responses;

[Trait("Animal", "Responses")]
public sealed class AnimalResponseTests
{
    [Fact(DisplayName = "Constructor Returns AnimalResponse")]
    public void Constructor_Should_ReturnAnimalResponse_WhenCalledWithValidParameters()
    {
        // Arrange
        AnimalResponse expectedAnimal = AnimalDataFaker.GenerateValidAnimalResponse();

        // Act
        var actualAnimal = new AnimalResponse(
            expectedAnimal.Id,
            expectedAnimal.OrganizationId,
            expectedAnimal.Name,
            expectedAnimal.Species,
            expectedAnimal.Breed,
            expectedAnimal.Gender,
            expectedAnimal.Neutered,
            expectedAnimal.Color,
            expectedAnimal.Size,
            expectedAnimal.Age,
            expectedAnimal.OrganizationName,
            expectedAnimal.State,
            expectedAnimal.City,
            expectedAnimal.Description,
            expectedAnimal.Note,
            expectedAnimal.PixKey,
            expectedAnimal.Photo,
            expectedAnimal.CreatedAt,
            expectedAnimal.UpdatedAt
        );

        // Assert
        actualAnimal.Should().BeEquivalentTo(expectedAnimal, options =>
            options.Excluding(a => a.Id)
                .Excluding(a => a.CreatedAt)
                .Excluding(a => a.UpdatedAt));
    }
}
