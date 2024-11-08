using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Queries.GetById;

namespace SGONGA.WebAPI.API.Tests.Animals.Queries.GetById;

[Trait("Animal", "Query - GetById")]
public class GetAnimalByIdQueryTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var expectedQuery = new GetAnimalByIdQuery(id, true);

        // Act
        var actualQuery = new GetAnimalByIdQuery(id, true);

        // Assert
        actualQuery.Should().BeEquivalentTo(expectedQuery);
    }

    [Fact(DisplayName = "Constructor Initialize With Default TenantFilter")]
    public void Constructor_Should_InitializeWithDefaultTenantFilter_WhenNotProvided()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var expectedQuery = new GetAnimalByIdQuery(id);

        // Act
        var actualQuery = new GetAnimalByIdQuery(id);

        // Assert
        actualQuery.TenantFilter.Should().BeFalse();
        actualQuery.Should().BeEquivalentTo(expectedQuery);
    }
}
