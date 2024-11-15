using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.People.Exceptions;

[Trait("People", "Exceptions")]
public sealed class PersonNotFoundExceptionTests
{
    [Fact]
    public async Task PersonNotFoundException_Should_Contain_Correct_Message()
    {
        // Arrange
        var personId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<PersonNotFoundException>(() =>
            throw new PersonNotFoundException(personId));

        // Assert
        Assert.Equal($"Pessoa com ID = {personId} não encontrada", exception.Message);
    }
}
