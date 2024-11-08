using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.People.Commands.Delete;

public sealed record DeletePersonCommand(Guid Id) : BaseCommand
{
    public override Result IsValid()
    {
        ValidationResult = new DeletePersonCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public const string IdRequired = "O Id é obrigatório.";
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}