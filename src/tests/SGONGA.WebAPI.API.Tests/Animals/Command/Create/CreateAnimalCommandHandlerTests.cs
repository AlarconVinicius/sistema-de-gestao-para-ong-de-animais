using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Mocks;
using static SGONGA.WebAPI.API.Animals.Commands.Create.CreateAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Create;

[Trait("Animal", "Handler - Create")]
public class CreateAnimalCommandHandlerTests
{
    private readonly Mock<IGenericUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public CreateAnimalCommandHandlerTests()
    {
        _unitOfWorkMock = new();
        _animalRepositoryMock = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return Validation Failures")]
    public async Task Handle_Should_ReturnValidationFailures_WhenCommandIsNotValid()
    {
        // Arrange
        CreateAnimalCommand command = AnimalDataFaker.GenerateInvalidCreateAnimalCommand();
        CreateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _tenantProvider.Object);

        // Act
        Result result = await handler.Handle(command, default);

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

    [Fact(DisplayName = "Handle Return TenantId Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenTenantIdIsNotValid()
    {
        // Arrange
        CreateAnimalCommand command = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        CreateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _tenantProvider.Object);

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

    [Fact(DisplayName = "Handle Return Ok")]
    public async Task Handle_Should_ReturnOk_WhenCommandIsValid()
    {
        // Arrange
        CreateAnimalCommand command = AnimalDataFaker.GenerateValidCreateAnimalCommand();
        CreateAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Result.Ok(Guid.NewGuid()));

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.InsertAsync(It.IsAny<Animal>(), It.IsAny<CancellationToken>()),
            Times.Once());

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once());

        result.IsSuccess.Should().BeTrue();
    }
}
