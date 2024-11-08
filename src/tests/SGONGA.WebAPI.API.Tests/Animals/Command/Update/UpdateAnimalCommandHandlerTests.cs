using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Errors;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using SGONGA.WebAPI.Mocks;
using System.Linq.Expressions;
using static SGONGA.WebAPI.API.Animals.Commands.Update.UpdateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Update;

[Trait("Animal", "Handler - Update")]
public class UpdateAnimalCommandHandlerTests
{
    private readonly Mock<IGenericUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public UpdateAnimalCommandHandlerTests()
    {
        _unitOfWorkMock = new();
        _animalRepositoryMock = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return Validation Failures")]
    public async Task Handle_Should_ReturnValidationFailures_WhenCommandIsNotValid()
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
        UpdateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        // Act
        Result result = await handler.Handle(command, default);

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

    [Fact(DisplayName = "Handle Return TenantId Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenTenantIdIsNotValid()
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
        UpdateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Error.NotFound("INVALID_TENANT_ID", "TenantId não encontrado."));

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        result.Errors.Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(Error.NotFound("INVALID_TENANT_ID", "TenantId não encontrado."));
        result.IsFailed.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animal Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenAnimalIsNull()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
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
        UpdateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Result.Ok(tenantId));
        _animalRepositoryMock
            .Setup(x => x.SearchAsync(It.IsAny<Expression<Func<Animal, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Animal)null!);

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        result.Errors.Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(AnimalErrors.AnimalNotFound(command.Id));
        result.IsFailed.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Ok")]
    public async Task Handle_Should_ReturnOk_WhenCommandIsValid()
    {
        // Arrange
        Animal oldAnimal = AnimalDataFaker.GenerateValidAnimal();
        var tenantId = oldAnimal.TenantId;
        var newCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        var baseCommand = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        var command = new UpdateAnimalCommand(
            oldAnimal.Id,
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
        UpdateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Result.Ok(tenantId));
        _animalRepositoryMock
            .Setup(x => x.SearchAsync(It.IsAny<Expression<Func<Animal, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(oldAnimal);

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.Update(It.IsAny<Animal>()),
            Times.Once());

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once());

        result.IsSuccess.Should().BeTrue();
    }
}
