using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Identity.Responses;

namespace SGONGA.WebAPI.API.Users.Commands.Login;

public class LoginUserCommandHandler(Identity.Handlers.IIdentityHandler IdentityHandler) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = command.IsValid();
        if (validationResult.IsFailed)
            return validationResult.Errors;

        var result = await IdentityHandler.LoginAsync(new(command.Email, command.Password));

        if (result.IsSuccess)
            return result.Value;

        return result.Errors;
    }
}