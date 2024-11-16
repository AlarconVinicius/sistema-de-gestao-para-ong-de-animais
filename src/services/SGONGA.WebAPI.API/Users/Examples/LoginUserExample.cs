using SGONGA.WebAPI.API.Users.Commands.Login;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.API.Users.Examples;

[ExcludeFromCodeCoverage]
public class LoginUserExample : IMultipleExamplesProvider<LoginUserCommand>
{
    IEnumerable<SwaggerExample<LoginUserCommand>> IMultipleExamplesProvider<LoginUserCommand>.GetExamples()
    {
        yield return SwaggerExample.Create(
            "Realizar login",
            new LoginUserCommand(
            Email: "user@email.com",
            Password: "P@ssword123"
           ));
    }
}