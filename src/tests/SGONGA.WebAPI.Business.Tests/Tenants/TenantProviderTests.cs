using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Handlers;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using SGONGA.WebAPI.Mocks.Handlers;
using SGONGA.WebAPI.Mocks.Repositories;

namespace SGONGA.WebAPI.Business.Tests.Tenants;

[Trait("Tenants", "Handlers")]
public class TenantProviderTests
{
    private readonly Mock<ITenantProvider> _mockTenantProvider = HandlerMocks.TenantProvider();
    private readonly Mock<IPersonRepository> _mockPersonRepository = RepositoryMocks.PersonRepository();

    [Fact]
    public async Task GetTenantId_Should_ReturnError_WhenTenantIdIsNotSet()
    {
        // Arrange
        var provider = new TenantProvider(_mockPersonRepository.Object);

        // Act
        var result = await provider.GetTenantId();

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("TENANT_ID_NOT_FOUND", result.Errors[0].Code);
    }

    [Fact]
    public async Task GetTenantId_Should_ReturnError_WhenTenantIdDoesNotExist()
    {
        // Arrange
        Guid tenantId = Guid.NewGuid();
        var provider = new TenantProvider(_mockPersonRepository.Object);
        provider.SetTenantId(CreateHeaderDictionary(tenantId.ToString()));

        _mockPersonRepository.ExistsAsync()
            .ReturnsAsync(false);

        // Act
        var result = await provider.GetTenantId();

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("INVALID_TENANT_ID", result.Errors[0].Code);
    }

    [Fact]
    public async Task GetTenantId_Should_ReturnId_WhenTenantIdExists()
    {
        // Arrange
        Guid tenantId = Guid.NewGuid();
        var provider = new TenantProvider(_mockPersonRepository.Object);
        provider.SetTenantId(CreateHeaderDictionary(tenantId.ToString()));

        _mockPersonRepository.ExistsAsync()
        .ReturnsAsync(true);

        // Act
        var result = await provider.GetTenantId();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(tenantId, result.Value);
    }

    [Fact]
    public void SetTenantId_Should_ReturnError_WhenTenantIdHeaderIsMissing()
    {
        // Arrange
        var provider = new TenantProvider(_mockPersonRepository.Object);
        var headers = new HeaderDictionary();

        // Act
        var result = provider.SetTenantId(headers);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("INVALID_TENANT_ID", result.Errors[0].Code);
    }

    [Fact]
    public void SetTenantId_Should_ReturnError_WhenTenantIdFormatIsInvalid()
    {
        // Arrange
        var provider = new TenantProvider(_mockPersonRepository.Object);
        var headers = CreateHeaderDictionary("invalid-guid");

        // Act
        var result = provider.SetTenantId(headers);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("INVALID_TENANT_ID_FORMAT", result.Errors[0].Code);
    }

    [Fact]
    public async Task SetTenantId_Should_SetTenantId_WhenHeaderIsValid()
    {
        // Arrange
        Guid tenantId = Guid.NewGuid();
        var provider = new TenantProvider(_mockPersonRepository.Object);
        var headers = CreateHeaderDictionary(tenantId.ToString());

        _mockPersonRepository.ExistsAsync()
        .ReturnsAsync(true);

        // Act
        var result = provider.SetTenantId(headers);

        // Assert
        Assert.True(result.IsSuccess);

        // Verify the TenantId was set correctly
        var getTenantIdResult = await provider.GetTenantId();
        Assert.True(getTenantIdResult.IsSuccess);
        Assert.Equal(tenantId, getTenantIdResult.Value);
    }

    private IHeaderDictionary CreateHeaderDictionary(string tenantIdValue)
    {
        var headers = new HeaderDictionary
            {
                { "TenantId", new StringValues(tenantIdValue) }
            };
        return headers;
    }
}