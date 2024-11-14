using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.People.Errors;

public static class PersonErrors
{
    public static Error UserNotFound(Guid id) => Error.NotFound("USER_NOT_FOUND", $"The user with Id = '{id}' was not found.");
    public static readonly Error NoUsersFound = Error.NotFound("NO_USERS_FOUND", "No users were found.");

    public static Error EmailInUse(string email) => Error.Conflict("EMAIL_IN_USE", $"The email = '{email}' is already in use.");
    public static Error NicknameInUse(string nickname) => Error.Conflict("NICKNAME_IN_USE", $"The nickname = '{nickname}' is already in use.");
    public static Error DocumentInUse(string document) => Error.Conflict("DOCUMENT_IN_USE", $"The document = '{document}' is already in use.");

    public static readonly Error FailedToRetrieveUser = Error.Failure("FAILED_TO_RETRIEVE_USER", "Failed to retrieve the user.");
    public static readonly Error FailedToRetrieveUsers = Error.Failure("FAILED_TO_RETRIEVE_USERS", "Failed to retrieve users.");
    public static readonly Error FailedToCreateUser = Error.Failure("FAILED_TO_CREATE_USER", "Failed to create the user.");
    public static readonly Error FailedToUpdateUser = Error.Failure("FAILED_TO_UPDATE_USER", "Failed to update the user.");
    public static readonly Error FailedToDeleteUser = Error.Failure("FAILED_TO_DELETE_USER", "Failed to delete the user.");
}
