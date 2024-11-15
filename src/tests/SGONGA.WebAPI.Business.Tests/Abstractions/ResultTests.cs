using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Tests.Abstractions;

[Trait("Abstractions", "Result")]
public sealed class ResultTests
{
    [Fact]
    public void Ok_Should_Return_SuccessfulResult()
    {
        // Act
        var result = Result.Ok();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailed);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Fail_Should_Return_FailedResult_With_SingleError()
    {
        // Arrange
        var error = Error.Forbidden("FORBIDDEN", "ErrorMessage");

        // Act
        var result = Result.Fail(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors[0]);
    }

    [Fact]
    public void Fail_Should_Throw_ArgumentNullException_When_Error_IsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Result.Fail((Error)null!));
    }

    [Fact]
    public void Fail_Should_Throw_ArgumentNullException_When_Errors_AreNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Result.Fail((IEnumerable<Error>)null!));
    }

    [Fact]
    public void Fail_Should_Return_FailedResult_With_MultipleErrors()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Forbidden("FORBIDDEN", "ErrorMessage1"),
            Error.Forbidden("FORBIDDEN", "ErrorMessage2")
        };

        // Act
        var result = Result.Fail(errors);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.Equal(errors, result.Errors);
    }

    [Fact]
    public void OkTValue_Should_Return_SuccessfulResult_With_Value()
    {
        // Arrange
        var value = "TestValue";

        // Act
        var result = Result.Ok(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailed);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void FailTValue_Should_Return_FailedResult_With_SingleError()
    {
        // Arrange
        var error = Error.Forbidden("FORBIDDEN", "ErrorMessage");

        // Act
        var result = Result.Fail<string>(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors[0]);
    }

    [Fact]
    public void FailTValue_Should_Throw_ArgumentNullException_When_Error_IsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Result.Fail<string>((IEnumerable<Error>)null!));
    }

    [Fact]
    public void FailTValue_Should_Throw_ArgumentNullException_When_Errors_AreNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Result.Fail<string>(Enumerable.Empty<Error>()));
    }

    [Fact]
    public void FailTValue_Should_Throw_InvalidOperationException_When_Accessing_Value()
    {
        // Arrange
        var error = Error.Forbidden("FORBIDDEN", "ErrorMessage");
        var result = Result.Fail<string>(error);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }
}
