using FluentValidation;
using SGONGA.WebAPI.API.Animals.Shared;

namespace SGONGA.WebAPI.API.Animals.Command.Update;

public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("O Id é obrigatório.");
        Include(new BaseAnimalCommandValidator());
    }
}
