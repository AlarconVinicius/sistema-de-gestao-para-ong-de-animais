using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Commands.Delete;
using SGONGA.WebAPI.API.Animals.Commands.Update;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Mocks;
using System.Linq.Expressions;
using static SGONGA.WebAPI.API.Animals.Commands.Delete.DeleteAnimalCommand;

namespace SGONGA.WebAPI.API.Tests.Animals.Command.Delete;

[Trait("Animal", "Handler - Delete")]
public class DeleteAnimalCommandHandlerTests
{
    private readonly Mock<IGenericUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public DeleteAnimalCommandHandlerTests()
    {
        _unitOfWorkMock = new();
        _animalRepositoryMock = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return Validation Failures")]
    public async Task Handle_Should_ReturnValidationFailures_WhenCommandIsNotValid()
    {
        // Arrange
        var command = new DeleteAnimalCommand(Guid.Empty);
        DeleteAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().HaveCount(1);

        result.Errors.Should().BeEquivalentTo(new List<Error>
        {
            Error.Validation(DeleteAnimalCommandValidator.IdRequired)
        });
    }

    [Fact(DisplayName = "Handle Return TenantId Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenTenantIdIsNotValid()
    {
        // Arrange
        var command = new DeleteAnimalCommand(Guid.NewGuid());
        DeleteAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

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
        var command = new DeleteAnimalCommand(Guid.NewGuid());
        DeleteAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Result.Ok(tenantId));
        _animalRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
            .ReturnsAsync(false);

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
        var tenantId = Guid.NewGuid();
        var command = new DeleteAnimalCommand(Guid.NewGuid());
        DeleteAnimalCommandHandler handler = new(_unitOfWorkMock.Object, _animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Result.Ok(tenantId));
        _animalRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Animal, bool>>>()))
            .ReturnsAsync(true);

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.Delete(It.IsAny<Animal>()),
            Times.Once());

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once());

        result.IsSuccess.Should().BeTrue();
    }
}
