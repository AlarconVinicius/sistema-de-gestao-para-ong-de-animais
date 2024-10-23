using FluentAssertions;
using Moq;
using SGONGA.WebAPI.API.Animals.Queries.GetById;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.API.Tests.Animals.Queries.GetById;

[Trait("Animal", "Queries")]
public class GetAnimalByIdQueryHandlerTests
{
    private readonly Mock<IAnimalRepository> _animalRepository;
    private readonly Mock<ITenantProvider> _tenantProvider;

    public GetAnimalByIdQueryHandlerTests()
    {
        _animalRepository = new();
        _tenantProvider = new();
    }

    [Fact(DisplayName = "Handle Return Animal Filtered By Tenant")]
    public async Task Handle_Should_Return_AnimalFilteredByTenant_WhenQueryIsValid()
    {
        // Arrange
        GetAnimalByIdQuery query = new(Guid.NewGuid(), true);
        Guid tenantId = Guid.NewGuid();
        AnimalResponse animalResponse = new(query.Id, tenantId, "animal", "especie", "raca", true, true, "cor", "porte", "idade", "ong", "endereco", "descricao", "observacao", "chavepix", "foto", DateTime.UtcNow, DateTime.UtcNow);
        GetAnimalByIdQueryHandler handler = new(_animalRepository.Object, _tenantProvider.Object);

        _tenantProvider.Setup(
            x => x.GetTenantId())
            .ReturnsAsync(tenantId);

        _animalRepository.Setup(
            x => x.GetByIdAsync(query.Id, tenantId, default))
            .ReturnsAsync(animalResponse);

        // Act
        Result<AnimalResponse> result = await handler.Handle(query, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(animalResponse);
    }
}
