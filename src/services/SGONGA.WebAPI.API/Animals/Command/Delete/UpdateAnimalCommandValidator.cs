using FluentValidation;

namespace SGONGA.WebAPI.API.Animals.Command.Delete;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("O Id é obrigatório.");
    }
}
