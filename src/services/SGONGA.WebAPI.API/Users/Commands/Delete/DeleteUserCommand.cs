using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Users.Commands.Delete;

public sealed record DeleteUserCommand(Guid Id) : BaseCommand
{
    public override Result IsValid()
    {
        ValidationResult = new DeleteUserCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public const string IdRequired = "O Id é obrigatório.";
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}