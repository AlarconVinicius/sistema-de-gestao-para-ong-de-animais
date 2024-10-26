using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Queries.GetAll;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Mocks;

namespace SGONGA.WebAPI.API.Tests.Animals.Queries.GetAll;

[Trait("Animal", "Handler - GetAll")]
public class GetAllAnimalsQueryHandlerTests
{
    private readonly Mock<IAnimalRepository> _animalRepositoryMock;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public GetAllAnimalsQueryHandlerTests()
    {
        _animalRepositoryMock = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return TenantId Not Found Error")]
    public async Task Handle_Should_ReturnNotFoundError_WhenTenantIdIsNotValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var query = new GetAllAnimalsQuery(50, 1, null, false, true);
        GetAllAnimalsQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

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

    [Fact(DisplayName = "Handle Return Animals Empty Filtered By Tenant")]
    public async Task Handle_Should_Return_AnimalsEmpty_WhenFilteredByTenantAndThereIsNoAnimal()
    {
        // Arrange
        GetAllAnimalsQuery query = new(50, 1, null, false, true);
        Guid tenantId = Guid.NewGuid();
        GetAllAnimalsQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider.Setup(
            x => x.GetTenantId())
            .ReturnsAsync(tenantId);

        _animalRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BasePagedResponse<AnimalResponse>)null!);

        // Act
        Result<BasePagedResponse<AnimalResponse>> result = await handler.Handle(query, default);

        // Assert
        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animals Filtered By Tenant")]
    public async Task Handle_Should_Return_AnimalsFilteredByTenant_WhenThereIsAnAnimal()
    {
        // Arrange
        GetAllAnimalsQuery query = new(50, 1, null, false, true);
        Guid tenantId = Guid.NewGuid();
        var animalsResponse = AnimalDataFaker.GenerateValidAnimalsResponse(3);
        GetAllAnimalsQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _tenantProvider.Setup(
            x => x.GetTenantId())
            .ReturnsAsync(tenantId);

        _animalRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(animalsResponse);

        // Act
        Result<BasePagedResponse<AnimalResponse>> result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.List.Should().NotBeNull();
        result.Value.List.Should().HaveSameCount(animalsResponse.List);
    }

    [Fact(DisplayName = "Handle Return Animals Empty")]
    public async Task Handle_Should_Return_AnimalsEmpty_WhenThereIsNoAnimal()
    {
        // Arrange
        GetAllAnimalsQuery query = new(50, 1, null, false, false);
        GetAllAnimalsQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _animalRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BasePagedResponse<AnimalResponse>)null!);

        // Act
        Result<BasePagedResponse<AnimalResponse>> result = await handler.Handle(query, default);

        // Assert
        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact(DisplayName = "Handle Return Animals")]
    public async Task Handle_Should_Return_Animal_WhenThereIsAnAnimal()
    {
        // Arrange
        GetAllAnimalsQuery query = new(50, 1, null, false, false);
        BasePagedResponse<AnimalResponse> animalsResponse = AnimalDataFaker.GenerateValidAnimalsResponse(3);
        GetAllAnimalsQueryHandler handler = new(_animalRepositoryMock.Object, _tenantProvider.Object);

        _animalRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(animalsResponse);

        // Act
        Result<BasePagedResponse<AnimalResponse>> result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.List.Should().NotBeNull();
        result.Value.List.Should().HaveSameCount(animalsResponse.List);
    }

}
