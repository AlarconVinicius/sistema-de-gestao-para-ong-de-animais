using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Tests.Abstractions;

[Trait("Abstractions", "Result")]
public class ErrorTests
{
    [Fact]
    public void NotFound_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string code = "RESOURCE_NOT_FOUND";
        string message = "The requested resource was not found.";

        // Act
        var error = Error.NotFound(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.NotFound, error.Type);
    }

    [Fact]
    public void Validation_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string message = "The value is invalid.";

        // Act
        var error = Error.Validation(message);

        // Assert
        Assert.Equal("VALIDATION_ERROR", error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Validation, error.Type);
    }

    [Fact]
    public void Validation_WithCode_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string code = "INVALID_INPUT";
        string message = "The input provided is invalid.";

        // Act
        var error = Error.Validation(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Validation, error.Type);
    }

    [Fact]
    public void Conflict_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string code = "DUPLICATE_RESOURCE";
        string message = "A resource with the same identifier already exists.";

        // Act
        var error = Error.Conflict(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Conflict, error.Type);
    }

    [Fact]
    public void Failure_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string code = "UNEXPECTED_ERROR";
        string message = "An unexpected error occurred.";

        // Act
        var error = Error.Failure(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Failure, error.Type);
    }

    [Fact]
    public void Forbidden_Should_ReturnError_WithCorrectProperties()
    {
        // Arrange
        string code = "ACCESS_FORBIDDEN";
        string message = "You are not allowed to access this resource.";

        // Act
        var error = Error.Forbidden(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Forbidden, error.Type);
    }

    [Fact]
    public void PredefinedErrors_Should_HaveCorrectProperties()
    {
        // Act & Assert
        Assert.Equal(string.Empty, Error.None.Code);
        Assert.Equal(string.Empty, Error.None.Message);
        Assert.Equal(ErrorType.Failure, Error.None.Type);

        Assert.Equal("NULL_VALUE", Error.NullValue.Code);
        Assert.Equal("Um valor nulo foi fornecido.", Error.NullValue.Message);
        Assert.Equal(ErrorType.Failure, Error.NullValue.Type);

        Assert.Equal("CONDITION_NOT_MET", Error.ConditionNotMet.Code);
        Assert.Equal("A condição especificada não foi atendida.", Error.ConditionNotMet.Message);
        Assert.Equal(ErrorType.Failure, Error.ConditionNotMet.Type);

        Assert.Equal("ACCESS_DENIED", Error.AccessDenied.Code);
        Assert.Equal("Você não tem permissão para acessar este recurso.", Error.AccessDenied.Message);
        Assert.Equal(ErrorType.Forbidden, Error.AccessDenied.Type);

        Assert.Equal("COMMIT_FAILED", Error.CommitFailed.Code);
        Assert.Equal("Nenhuma alteração foi confirmada no banco de dados.", Error.CommitFailed.Message);
        Assert.Equal(ErrorType.Conflict, Error.CommitFailed.Type);
    }
}