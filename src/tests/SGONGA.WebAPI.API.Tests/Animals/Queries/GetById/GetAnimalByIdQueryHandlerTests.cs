using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.API.Animals.Queries.GetById;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.API.Tests.Animals.Queries.GetById;

[Trait("Animal", "Handler - GetById")]
public class GetAnimalByIdQueryHandlerTests
{
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public GetAnimalByIdQueryHandlerTests()
    {
        _animalRepositoryMock = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return TenantId Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenTenantIdIsNotValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var query = new GetAnimalByIdQuery(id, true);
        GetAnimalByIdQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider
            .Setup(x => x.GetTenantId())
            .ReturnsAsync(Error.NotFound("INVALID_TENANT_ID", "TenantId não encontrado."));

        // Act
        Result result = await handler.Handle(query, default);

        // Assert
        result.Errors.Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(Error.NotFound("INVALID_TENANT_ID", "TenantId não encontrado."));
        result.IsFailed.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animal Not Found Filtered By Tenant")]
    public async Task Handle_Should_Return_AnimalNotFound_WhenFilteredByTenantAndAnimalDoesntExist()
    {
        // Arrange
        GetAnimalByIdQuery query = new(Guid.NewGuid(), true);
        Guid tenantId = Guid.NewGuid();
        GetAnimalByIdQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider.Setup(
            x => x.GetTenantId())
            .ReturnsAsync(tenantId);

        _animalRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AnimalResponse)null!);

        // Act
        Result<AnimalResponse> result = await handler.Handle(query, default);

        // Assert
        result.Errors.Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(AnimalErrors.AnimalNotFound(query.Id));
        result.IsFailed.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animal Filtered By Tenant")]
    public async Task Handle_Should_Return_AnimalFilteredByTenant_WhenAnimalExists()
    {
        // Arrange
        GetAnimalByIdQuery query = new(Guid.NewGuid(), true);
        Guid tenantId = Guid.NewGuid();
        AnimalResponse animalResponse = AnimalDataFaker.GenerateValidAnimalResponse();
        GetAnimalByIdQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider.Setup(
            x => x.GetTenantId())
            .ReturnsAsync(tenantId);

        _animalRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(animalResponse);

        // Act
        Result<AnimalResponse> result = await handler.Handle(query, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(animalResponse);
    }

    [Fact(DisplayName = "Handle Return Animal Not Found")]
    public async Task Handle_Should_Return_AnimalNotFound_WhenAnimalDoesntExist()
    {
        // Arrange
        GetAnimalByIdQuery query = new(Guid.NewGuid(), false);
        GetAnimalByIdQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _animalRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AnimalResponse)null!);

        // Act
        Result<AnimalResponse> result = await handler.Handle(query, default);

        // Assert
        result.Errors.Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(AnimalErrors.AnimalNotFound(query.Id));
        result.IsFailed.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animal")]
    public async Task Handle_Should_Return_Animal_WhenAnimalExists()
    {
        // Arrange
        GetAnimalByIdQuery query = new(Guid.NewGuid(), false);
        AnimalResponse animalResponse = AnimalDataFaker.GenerateValidAnimalResponse();
        GetAnimalByIdQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _animalRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(animalResponse);

        // Act
        Result<AnimalResponse> result = await handler.Handle(query, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(animalResponse);
    }

}
