using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Language.Flow;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.Mocks.Handlers;

[ExcludeFromCodeCoverage]
public static class HandlerMocks
{
    public static Mock<ITenantProvider> TenantProvider() => new();
    public static Mock<IPersonRepository> PersonRepository() => new();
}

[ExcludeFromCodeCoverage]
public static class MockTenantProviderExtension
{
    public static ISetup<ITenantProvider, Task<Result<Guid>>> GetTenantId(this Mock<ITenantProvider> mock) =>
        mock.Setup(m => m.GetTenantId());

    public static ISetup<ITenantProvider, Result> SetTenantId(this Mock<ITenantProvider> mock) =>
        mock.Setup(m => m.SetTenantId(It.IsAny<IHeaderDictionary>()));

    public static ISetup<ITenantProvider, Result> SetTenantId(this Mock<ITenantProvider> mock, IHeaderDictionary headerDictionary) =>
        mock.Setup(m => m.SetTenantId(headerDictionary));
}