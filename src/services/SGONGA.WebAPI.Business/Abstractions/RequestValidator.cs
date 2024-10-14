using FluentValidation;
using FluentValidation.Results;

namespace SGONGA.WebAPI.Business.Abstractions;
public static class RequestValidator
{
    public static Result IsValid<TRequest>(TRequest request, AbstractValidator<TRequest> validator)
    {
        if (request == null)
        {
            return Error.Failure("VALIDATION_ERRORS", "Os dados informados são inválidos");
        }

        ValidationResult validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(x => Error.Validation("VALIDATION_ERRORS", x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }
}