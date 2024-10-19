using FluentValidation;
using SGONGA.WebAPI.API.Animals.Shared;

namespace SGONGA.WebAPI.API.Animals.Command.Create;

public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator()
    {
        Include(new BaseAnimalCommandValidator());
    }
}
