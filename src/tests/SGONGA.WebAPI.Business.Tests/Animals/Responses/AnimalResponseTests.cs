using FluentAssertions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.Business.Tests.Animals.Models;

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
            expectedAnimal.OngId,
            expectedAnimal.Nome,
            expectedAnimal.Especie,
            expectedAnimal.Raca,
            expectedAnimal.Sexo,
            expectedAnimal.Castrado,
            expectedAnimal.Cor,
            expectedAnimal.Porte,
            expectedAnimal.Idade,
            expectedAnimal.Ong,
            expectedAnimal.Endereco,
            expectedAnimal.Descricao,
            expectedAnimal.Observacao,
            expectedAnimal.ChavePix,
            expectedAnimal.Foto,
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
