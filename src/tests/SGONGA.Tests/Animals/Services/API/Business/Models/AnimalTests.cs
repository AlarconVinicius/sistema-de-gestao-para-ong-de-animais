using SGONGA.Tests.Animals.Shared;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.Tests.Animals.Services.API.Business.Models;

[Trait("Animal", "Models")]
public sealed class AnimalTests
{
    [Fact]
    public void Constructor_Empty_InitializesWithDefaultValues()
    {
        // Act
        var animal = new Animal();

        // Assert
        Assert.NotNull(animal);
        Assert.Equal(Guid.Empty, animal.TenantId);
        Assert.Equal(string.Empty, animal.Nome);
        Assert.Equal(string.Empty, animal.Especie);
        Assert.Equal(string.Empty, animal.Raca);
        Assert.False(animal.Sexo);
        Assert.False(animal.Castrado);
        Assert.Equal(string.Empty, animal.Cor);
        Assert.Equal(string.Empty, animal.Porte);
        Assert.Equal(string.Empty, animal.Idade);
        Assert.Equal(string.Empty, animal.Descricao);
        Assert.Equal(string.Empty, animal.Observacao);
        Assert.Equal(string.Empty, animal.ChavePix);
        Assert.Null(animal.ONG);
    }

    [Fact]
    public void Create_ValidParameters_CreatesAnimal()
    {
        // Arrange
        Animal expectedAnimal = AnimalDataFaker.GenerateValidData();

        // Act
        var actualAnimal = Animal.Create(
            expectedAnimal.TenantId,
            expectedAnimal.Nome,
            expectedAnimal.Especie,
            expectedAnimal.Raca,
            expectedAnimal.Sexo,
            expectedAnimal.Castrado,
            expectedAnimal.Cor,
            expectedAnimal.Porte,
            expectedAnimal.Idade,
            expectedAnimal.Descricao,
            expectedAnimal.Observacao,
            expectedAnimal.Foto,
            expectedAnimal.ChavePix
        );

        // Assert
        Assert.Equal(expectedAnimal.TenantId, actualAnimal.TenantId);
        Assert.Equal(expectedAnimal.Nome, actualAnimal.Nome);
        Assert.Equal(expectedAnimal.Especie, actualAnimal.Especie);
        Assert.Equal(expectedAnimal.Raca, actualAnimal.Raca);
        Assert.Equal(expectedAnimal.Sexo, actualAnimal.Sexo);
        Assert.Equal(expectedAnimal.Castrado, actualAnimal.Castrado);
        Assert.Equal(expectedAnimal.Cor, actualAnimal.Cor);
        Assert.Equal(expectedAnimal.Porte, actualAnimal.Porte);
        Assert.Equal(expectedAnimal.Idade, actualAnimal.Idade);
        Assert.Equal(expectedAnimal.Descricao, actualAnimal.Descricao);
        Assert.Equal(expectedAnimal.Observacao, actualAnimal.Observacao);
        Assert.Equal(expectedAnimal.Foto, actualAnimal.Foto);
        Assert.Equal(expectedAnimal.ChavePix, actualAnimal.ChavePix);
    }

    [Fact]
    public void Create_InvalidParameters_ThrowsArgumentException()
    {
        // Arrange
        var (tenantId, nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix) = AnimalDataFaker.GenerateInvalidData();

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() =>
        {
            Animal.Create(
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
        });

        Assert.Contains("TenantId não pode ser vazio.", exception.Message);
        Assert.Contains("Nome não pode ser nulo ou vazio.", exception.Message);
        Assert.Contains("Espécie não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Raça não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Cor não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Porte não pode ser nulo ou vazio.", exception.Message);
        Assert.Contains("Descrição não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Observação não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Foto não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Idade não pode ser nula ou vazia.", exception.Message);
    }

    [Fact]
    public void Update_ValidParameters_UpdatesAnimal()
    {
        // Arrange
        Animal expectedAnimal = AnimalDataFaker.GenerateValidData();
        Animal actualAnimal = AnimalDataFaker.GenerateValidData();

        // Act
        actualAnimal.Update(
            expectedAnimal.Nome,
            expectedAnimal.Especie,
            expectedAnimal.Raca,
            expectedAnimal.Sexo,
            expectedAnimal.Castrado,
            expectedAnimal.Cor,
            expectedAnimal.Porte,
            expectedAnimal.Idade,
            expectedAnimal.Descricao,
            expectedAnimal.Observacao,
            expectedAnimal.Foto,
            expectedAnimal.ChavePix);

        // Assert
        Assert.Equal(expectedAnimal.Nome, actualAnimal.Nome);
        Assert.Equal(expectedAnimal.Especie, actualAnimal.Especie);
        Assert.Equal(expectedAnimal.Raca, actualAnimal.Raca);
        Assert.Equal(expectedAnimal.Sexo, actualAnimal.Sexo);
        Assert.Equal(expectedAnimal.Castrado, actualAnimal.Castrado);
        Assert.Equal(expectedAnimal.Cor, actualAnimal.Cor);
        Assert.Equal(expectedAnimal.Porte, actualAnimal.Porte);
        Assert.Equal(expectedAnimal.Idade, actualAnimal.Idade);
        Assert.Equal(expectedAnimal.Descricao, actualAnimal.Descricao);
        Assert.Equal(expectedAnimal.Observacao, actualAnimal.Observacao);
        Assert.Equal(expectedAnimal.Foto, actualAnimal.Foto);
        Assert.Equal(expectedAnimal.ChavePix, actualAnimal.ChavePix);
    }
    [Fact]
    public void Update_InvalidParameters_ThrowsArgumentException()
    {
        // Arrange
        Animal validAnimal = AnimalDataFaker.GenerateValidData();
        var (tenantId, nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix) = AnimalDataFaker.GenerateInvalidData();

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() =>
        {
            validAnimal.Update(nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix);
        });

        Assert.Contains("Nome não pode ser nulo ou vazio.", exception.Message);
        Assert.Contains("Espécie não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Raça não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Cor não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Porte não pode ser nulo ou vazio.", exception.Message);
        Assert.Contains("Descrição não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Observação não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Foto não pode ser nula ou vazia.", exception.Message);
        Assert.Contains("Idade não pode ser nula ou vazia.", exception.Message);
    }
}
