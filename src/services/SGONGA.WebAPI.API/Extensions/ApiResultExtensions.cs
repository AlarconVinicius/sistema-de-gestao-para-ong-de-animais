 using Microsoft.AspNetCore.Mvc.ModelBinding;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Extensions;

public static class ApiResultExtensions
{
    public static IResult ToProblemDetails(this ModelStateDictionary modelState)
    {
        var erros = modelState.Values
            .SelectMany(ms => ms.Errors)
            .Select(e => new { message = e.ErrorMessage })
            .ToArray();

        var errors = new Dictionary<string, object?>
        {
            { "errors", erros }
        };

        return Results.Problem(
            statusCode: GetStatusCode(ErrorType.Validation),
            title: GetTitle(ErrorType.Validation),
            type: GetType(ErrorType.Validation),
            extensions: errors
        );
    }
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Não é possível gerar um ProblemDetails para um resultado de sucesso.");
        }

        var firstError = result.Errors.FirstOrDefault();

        if (firstError == null)
            throw new InvalidOperationException("O resultado falhou, mas não contém erros.");

        return Results.Problem(
            statusCode: GetStatusCode(firstError.Type),
            title: GetTitle(firstError.Type),
            type: GetType(firstError.Type),
            extensions: GetErrors(result)
        );
    }
    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Forbidden => "Forbidden",
            _ => "Server Error"
        };

    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

    private static Dictionary<string, object?>? GetErrors(Result result)
    {
        if (!result.Errors.Any())
        {
            return null;
        }

        // Retornando uma lista de erros no formato esperado
        return new Dictionary<string, object?>
        {
            { "errors", result.Errors.Select(e => new { message = e.Message }).ToArray() }
        };
    }
}
