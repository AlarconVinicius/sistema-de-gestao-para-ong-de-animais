using FluentAssertions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Shared.Exceptions;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.Business.Tests.Animals.Entities;

[Trait("Animals", "Entities")]
public sealed class AnimalTests
{
    [Fact(DisplayName = "Constructor initializes with default values when called with no parameters")]
    public void Constructor_Should_InitializeWithDefaultValues_WhenCalledWithNoParameters()
    {
        // Act
        var animal = new Animal();

        // Assert
        animal.Should().NotBeNull();
        animal.TenantId.Should().Be(Guid.Empty);
        animal.Name.Should().BeEmpty();
        animal.Species.Should().BeEmpty();
        animal.Breed.Should().BeEmpty();
        animal.Gender.Should().BeFalse();
        animal.Neutered.Should().BeFalse();
        animal.Color.Should().BeEmpty();
        animal.Size.Should().BeEmpty();
        animal.Age.Should().BeEmpty();
        animal.Description.Should().BeEmpty();
        animal.Note.Should().BeEmpty();
        animal.PixKey.Should().BeEmpty();
        animal.Organization.Should().BeNull();
    }

    [Fact(DisplayName = "Create returns animal when called with valid parameters")]
    public void Create_Should_ReturnAnimal_WhenCalledWithValidParameters()
    {
        // Arrange
        Animal expectedAnimal = AnimalDataFaker.GenerateValidAnimal();

        // Act
        var actualAnimal = Animal.Create(
            expectedAnimal.TenantId,
            expectedAnimal.Name,
            expectedAnimal.Species,
            expectedAnimal.Breed,
            expectedAnimal.Gender,
            expectedAnimal.Neutered,
            expectedAnimal.Color,
            expectedAnimal.Size,
            expectedAnimal.Age,
            expectedAnimal.Description,
            expectedAnimal.Note,
            expectedAnimal.Photo,
            expectedAnimal.PixKey
        );

        // Assert
        actualAnimal.Should().BeEquivalentTo(expectedAnimal, options =>
            options.Excluding(a => a.Id)
                .Excluding(a => a.CreatedAt)
                .Excluding(a => a.UpdatedAt));
    }

    [Fact(DisplayName = "Create throws DomainException when called with invalid parameters")]
    public void Create_Should_ThrowDomainException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        var (tenantId, nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix) = AnimalDataFaker.GenerateInvalidAnimal();

        // Act
        Action action = () => Animal.Create(
            tenantId,
            nome,
            especie,
            raca,
            sexo,
            castrado,
            cor,
            porte,
            idade,
            descricao,
            observacao,
            foto,
            chavePix
        );

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("*")
            .Which.Message.Should().ContainAll(
                "TenantId não pode ser vazio.",
                "Nome não pode ser nulo ou vazio.",
                "Espécie não pode ser nula ou vazia.",
                "Raça não pode ser nula ou vazia.",
                "Cor não pode ser nula ou vazia.",
                "Porte não pode ser nulo ou vazio.",
                "Descrição não pode ser nula ou vazia.",
                "Observação não pode ser nula ou vazia.",
                "Foto não pode ser nula ou vazia.",
                "Idade não pode ser nula ou vazia."
            );
    }

    [Fact(DisplayName = "Update updates animal when called with valid parameters")]
    public void Update_Should_UpdateAnimal_WhenCalledWithValidParameters()
    {
        // Arrange
        Animal expectedAnimal = AnimalDataFaker.GenerateValidAnimal();
        Animal actualAnimal = AnimalDataFaker.GenerateValidAnimal();

        // Act
        actualAnimal.Update(
            expectedAnimal.Name,
            expectedAnimal.Species,
            expectedAnimal.Breed,
            expectedAnimal.Gender,
            expectedAnimal.Neutered,
            expectedAnimal.Color,
            expectedAnimal.Size,
            expectedAnimal.Age,
            expectedAnimal.Description,
            expectedAnimal.Note,
            expectedAnimal.Photo,
            expectedAnimal.PixKey
        );

        // Assert
        actualAnimal.Should().BeEquivalentTo(expectedAnimal, options =>
            options.Excluding(a => a.Id)
                .Excluding(a => a.TenantId)
                .Excluding(a => a.CreatedAt)
                .Excluding(a => a.UpdatedAt));
    }

    [Fact(DisplayName = "Update throws DomainException when called with invalid parameters")]
    public void Update_Should_ThrowDomainException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        Animal validAnimal = AnimalDataFaker.GenerateValidAnimal();
        var (tenantId, nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix) = AnimalDataFaker.GenerateInvalidAnimal();

        // Act
        Action action = () => validAnimal.Update(
            nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix
        );

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("*")
            .Which.Message.Should().ContainAll(
                "Nome não pode ser nulo ou vazio.",
                "Espécie não pode ser nula ou vazia.",
                "Raça não pode ser nula ou vazia.",
                "Cor não pode ser nula ou vazia.",
                "Porte não pode ser nulo ou vazio.",
                "Descrição não pode ser nula ou vazia.",
                "Observação não pode ser nula ou vazia.",
                "Foto não pode ser nula ou vazia.",
                "Idade não pode ser nula ou vazia."
            );
    }
}
