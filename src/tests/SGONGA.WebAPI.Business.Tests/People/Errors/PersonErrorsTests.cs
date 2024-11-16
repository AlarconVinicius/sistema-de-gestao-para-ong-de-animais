using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Errors;

namespace SGONGA.WebAPI.Business.Tests.People.Errors;

[Trait("People", "Errors")]
public sealed class PersonErrorsTests
{
    [Fact]
    public void UserNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var result = PersonErrors.UserNotFound(userId);

        // Assert
        Assert.Equal("USER_NOT_FOUND", result.Code);
        Assert.Equal($"The user with Id = '{userId}' was not found.", result.Message);
        Assert.Equal(ErrorType.NotFound, result.Type);
    }

    [Fact]
    public void NoUsersFound_ShouldReturnNotFoundError()
    {
        // Act
        var result = PersonErrors.NoUsersFound;

        // Assert
        Assert.Equal("NO_USERS_FOUND", result.Code);
        Assert.Equal("No users were found.", result.Message);
        Assert.Equal(ErrorType.NotFound, result.Type);
    }

    [Fact]
    public void EmailInUse_ShouldReturnConflictError()
    {
        // Arrange
        var email = "test@example.com";

        // Act
        var result = PersonErrors.EmailInUse(email);

        // Assert
        Assert.Equal("EMAIL_IN_USE", result.Code);
        Assert.Equal($"The email = '{email}' is already in use.", result.Message);
        Assert.Equal(ErrorType.Conflict, result.Type);
    }

    [Fact]
    public void NicknameInUse_ShouldReturnConflictError()
    {
        // Arrange
        var nickname = "john_doe";

        // Act
        var result = PersonErrors.NicknameInUse(nickname);

        // Assert
        Assert.Equal("NICKNAME_IN_USE", result.Code);
        Assert.Equal($"The nickname = '{nickname}' is already in use.", result.Message);
        Assert.Equal(ErrorType.Conflict, result.Type);
    }

    [Fact]
    public void DocumentInUse_ShouldReturnConflictError()
    {
        // Arrange
        var document = "12345678900";

        // Act
        var result = PersonErrors.DocumentInUse(document);

        // Assert
        Assert.Equal("DOCUMENT_IN_USE", result.Code);
        Assert.Equal($"The document = '{document}' is already in use.", result.Message);
        Assert.Equal(ErrorType.Conflict, result.Type);
    }

    [Fact]
    public void FailedToRetrieveUser_ShouldReturnFailureError()
    {
        // Act
        var result = PersonErrors.FailedToRetrieveUser;

        // Assert
        Assert.Equal("FAILED_TO_RETRIEVE_USER", result.Code);
        Assert.Equal("Failed to retrieve the user.", result.Message);
        Assert.Equal(ErrorType.Failure, result.Type);
    }

    [Fact]
    public void FailedToRetrieveUsers_ShouldReturnFailureError()
    {
        // Act
        var result = PersonErrors.FailedToRetrieveUsers;

        // Assert
        Assert.Equal("FAILED_TO_RETRIEVE_USERS", result.Code);
        Assert.Equal("Failed to retrieve users.", result.Message);
        Assert.Equal(ErrorType.Failure, result.Type);
    }

    [Fact]
    public void FailedToCreateUser_ShouldReturnFailureError()
    {
        // Act
        var result = PersonErrors.FailedToCreateUser;

        // Assert
        Assert.Equal("FAILED_TO_CREATE_USER", result.Code);
        Assert.Equal("Failed to create the user.", result.Message);
        Assert.Equal(ErrorType.Failure, result.Type);
    }

    [Fact]
    public void FailedToUpdateUser_ShouldReturnFailureError()
    {
        // Act
        var result = PersonErrors.FailedToUpdateUser;

        // Assert
        Assert.Equal("FAILED_TO_UPDATE_USER", result.Code);
        Assert.Equal("Failed to update the user.", result.Message);
        Assert.Equal(ErrorType.Failure, result.Type);
    }

    [Fact]
    public void FailedToDeleteUser_ShouldReturnFailureError()
    {
        // Act
        var result = PersonErrors.FailedToDeleteUser;

        // Assert
        Assert.Equal("FAILED_TO_DELETE_USER", result.Code);
        Assert.Equal("Failed to delete the user.", result.Message);
        Assert.Equal(ErrorType.Failure, result.Type);
    }
}