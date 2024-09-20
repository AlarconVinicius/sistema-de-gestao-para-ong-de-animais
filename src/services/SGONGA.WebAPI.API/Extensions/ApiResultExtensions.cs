using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Extensions;

public static class ApiResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Invalid error");
        }
        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: GetType(result.Error.Type),
            extensions: GetErrors(result)
        );

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Valdiation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

        static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Valdiation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                ErrorType.Forbidden => "Forbidden",
                _ => "Server Failure"
            };

        static string GetType(ErrorType statusCode) =>
            statusCode switch
            {
                ErrorType.Valdiation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static Dictionary<string, object?>? GetErrors(Result result)
        {
            //if (result.Error is not ValidationError validationError)
            //{
            //    return null;
            //}

            return new Dictionary<string, object?>
            {
                //{ "errors", validationError.Errors },
                { "errors", new[]  { result.Error } }
            };
        }
    }
}
