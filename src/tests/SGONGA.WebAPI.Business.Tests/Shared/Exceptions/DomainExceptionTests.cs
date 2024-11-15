using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.Shared.Exceptions;

[Trait("Shared", "Exceptions")]
public class DomainExceptionTests
{
    [Fact]
    public void DomainException_DefaultConstructor_Should_CreateInstance()
    {
        // Act
        var exception = new DomainException();

        // Assert
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
    }

    [Fact]
    public void DomainException_MessageConstructor_Should_SetMessage()
    {
        // Arrange
        string message = "This is a domain exception.";

        // Act
        var exception = new DomainException(message);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void DomainException_MessageAndInnerExceptionConstructor_Should_SetProperties()
    {
        // Arrange
        string message = "This is a domain exception.";
        var innerException = new InvalidOperationException("Inner exception message.");

        // Act
        var exception = new DomainException(message, innerException);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}
