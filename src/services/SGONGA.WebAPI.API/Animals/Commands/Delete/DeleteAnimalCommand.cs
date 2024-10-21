using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Commands.Delete;

public sealed record DeleteAnimalCommand(Guid Id) : BaseCommand
{
    public override Result IsValid()
    {
        ValidationResult = new DeleteAnimalCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }
    public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
    {
        public DeleteAnimalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O Id é obrigatório.");
        }
    }

}