using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Commands.Create;

public sealed record CreateAnimalCommand(
    string Name, 
    string Species,
    string Breed, 
    bool Gender,
    bool Neutered,
    string Color,
    string Size,
    string Age, 
    string Description, 
    string? Note, 
    string Photo,
    string? PixKey) : BaseAnimalCommand(Name, Species, Breed, Gender, Neutered, Color, Size, Age, Description, Note, Photo, PixKey)
{
    public override Result IsValid()
    {
        ValidationResult = new CreateAnimalCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class CreateAnimalCommandValidator : BaseAnimalCommandValidator<CreateAnimalCommand>
    {
        public CreateAnimalCommandValidator() { }
    }
}