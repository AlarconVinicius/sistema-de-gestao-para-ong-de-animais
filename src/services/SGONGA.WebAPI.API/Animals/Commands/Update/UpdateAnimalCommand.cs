using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Commands.Update;

public sealed record UpdateAnimalCommand : BaseAnimalCommand
{
    public UpdateAnimalCommand(
        Guid id,
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
        string? PixKey) : base(Name, Species, Breed, Gender, Neutered, Color, Size, Age, Description, Note, Photo, PixKey)
    {
        Id = id;
    }
    public Guid Id { get; set; }

    public override Result IsValid()
    {
        ValidationResult = new UpdateAnimalCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class UpdateAnimalCommandValidator : BaseAnimalCommandValidator<UpdateAnimalCommand>
    {
        public UpdateAnimalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}
