using FluentAssertions;
using SGONGA.WebAPI.API.Animals.Queries.GetAll;

namespace SGONGA.WebAPI.API.Tests.Animals.Queries.GetAll;

[Trait("Animals", "Queries")]
public class GetAllAnimalsQueryTests
{
    [Fact(DisplayName = "Constructor Initialize All Properties")]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        var expectedQuery = new GetAllAnimalsQuery(50, 1, "query", true, true);

        // Act
        var actualQuery = new GetAllAnimalsQuery(50, 1, "query", true, true);

        // Assert
        actualQuery.Should().BeEquivalentTo(expectedQuery);
    }

    [Fact(DisplayName = "Constructor Initialize With Default Properties")]
    public void Constructor_Should_InitializeWithDefaultProperties_WhenNotProvided()
    {
        // Arrange
        var expectedQuery = new GetAllAnimalsQuery();

        // Act
        var actualQuery = new GetAllAnimalsQuery(50, 1, null, false, false);

        // Assert
        actualQuery.TenantFilter.Should().BeFalse();
        actualQuery.Should().BeEquivalentTo(expectedQuery);
    }
}
