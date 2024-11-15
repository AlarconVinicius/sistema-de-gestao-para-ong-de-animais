using Moq;
using Moq.Language.Flow;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public static class RepositoryMocks
{
    public static Mock<IPersonRepository> PersonRepository() => new();
}

[ExcludeFromCodeCoverage]
public static class MockPersonRepositoryExtension
{
    public static ISetup<IPersonRepository, Task<bool>> ExistsAsync(this Mock<IPersonRepository> mock) =>
    mock.Setup(m => m.ExistsAsync(It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()));
}