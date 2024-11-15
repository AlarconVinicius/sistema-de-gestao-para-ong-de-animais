using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Tests.Abstractions;

[Trait("Abstractions", "Result")]
public class ResultExtensionsTests
{
    [Fact]
    public void Match_Should_Invoke_OnSuccess_For_SuccessfulResult()
    {
        // Arrange
        var result = Result.Ok();

        // Act
        var output = result.Match(
            onSuccess: () => "Success",
            onFailure: _ => "Failure");

        // Assert
        Assert.Equal("Success", output);
    }

    [Fact]
    public void Match_Should_Invoke_OnFailure_For_FailedResult()
    {
        // Arrange
        var error = Error.Forbidden("FORBIDDEN", "ErrorMessage");
        var result = Result.Fail(error);

        // Act
        var output = result.Match(
            onSuccess: () => "Success",
            onFailure: _ => "Failure");

        // Assert
        Assert.Equal("Failure", output);
    }

    [Fact]
    public void Match_WithValue_Should_Invoke_OnSuccess_For_SuccessfulResult_With_Value()
    {
        // Arrange
        var result = Result.Ok("SuccessValue");

        // Act
        var output = result.Match(
            onSuccess: value => $"Success: {value}",
            onFailure: _ => "Failure");

        // Assert
        Assert.Equal("Success: SuccessValue", output);
    }

    [Fact]
    public void Match_WithValue_Should_Invoke_OnFailure_For_FailedResult_With_Value()
    {
        // Arrange
        var error = Error.Forbidden("FORBIDDEN", "ErrorMessage");
        var result = Result.Fail<string>(error);

        // Act
        var output = result.Match(
            onSuccess: value => $"Success: {value}",
            onFailure: _ => "Failure");

        // Assert
        Assert.Equal("Failure", output);
    }
}