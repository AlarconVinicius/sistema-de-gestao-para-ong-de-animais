using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Animals.Commands;

public abstract record BaseAnimalCommand(
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
    string? PixKey) : BaseCommand
{
    public class BaseAnimalCommandValidator<T> : AbstractValidator<T> where T : BaseAnimalCommand
    {
        public const string IdRequired = "O Id é obrigatório.";
        public const string NameRequired = "O nome é obrigatório.";
        public const string SpeciesRequired = "A espécie é obrigatória.";
        public const string BreedRequired = "A raça é obrigatória.";
        public const string ColorRequired = "A cor é obrigatória.";
        public const string SizeRequired = "O porte é obrigatório.";
        public const string AgeRequired = "A idade é obrigatória.";
        public const string DescriptionRequired = "A descrição é obrigatória.";
        public const string NoteMaxLength = "A observação pode ter no máximo 500 caracteres.";
        public const string PhotoRequired = "A foto é obrigatória.";
        public const string PixKeyMaxLength = "A chave Pix pode ter no máximo 100 caracteres.";
        public const string NameMaxLength = "O nome pode ter no máximo 100 caracteres.";
        public const string SpeciesMaxLength = "A espécie pode ter no máximo 50 caracteres.";
        public const string BreedMaxLength = "A raça pode ter no máximo 50 caracteres.";
        public const string ColorMaxLength = "A cor pode ter no máximo 50 caracteres.";
        public const string SizeMaxLength = "O porte pode ter no máximo 50 caracteres.";
        public const string AgeMaxLength = "A idade pode ter no máximo 100 caracteres.";
        public const string DescriptionMaxLength = "A descrição pode ter no máximo 500 caracteres.";
        public const string ObservationMaxLength = "A observação pode ter no máximo 500 caracteres.";

        public BaseAnimalCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage(NameRequired)
                .MaximumLength(100).WithMessage(NameMaxLength);

            RuleFor(a => a.Species)
                .NotEmpty().WithMessage(SpeciesRequired)
                .MaximumLength(50).WithMessage(SpeciesMaxLength);

            RuleFor(a => a.Breed)
                .NotEmpty().WithMessage(BreedRequired)
                .MaximumLength(50).WithMessage(BreedMaxLength);

            RuleFor(a => a.Color)
                .NotEmpty().WithMessage(ColorRequired)
                .MaximumLength(50).WithMessage(ColorMaxLength);

            RuleFor(a => a.Size)
                .NotEmpty().WithMessage(SizeRequired)
                .MaximumLength(50).WithMessage(SizeMaxLength);

            RuleFor(a => a.Age)
                .NotEmpty().WithMessage(AgeRequired)
                .MaximumLength(100).WithMessage(AgeMaxLength);

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage(DescriptionRequired)
                .MaximumLength(500).WithMessage(DescriptionMaxLength);

            RuleFor(a => a.Note)
                .MaximumLength(500).WithMessage(NoteMaxLength);

            RuleFor(a => a.Photo)
                .NotEmpty().WithMessage(PhotoRequired);

            RuleFor(a => a.PixKey)
                .MaximumLength(100).WithMessage(PixKeyMaxLength);
        }
    }
}