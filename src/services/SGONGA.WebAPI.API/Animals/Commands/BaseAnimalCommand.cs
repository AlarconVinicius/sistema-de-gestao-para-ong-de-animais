using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Animals.Commands;

public abstract record BaseAnimalCommand(
    string Nome,
    string Especie,
    string Raca,
    bool Sexo,
    bool Castrado,
    string Cor,
    string Porte,
    string Idade,
    string Descricao,
    string Observacao,
    string Foto,
    string ChavePix = "") : BaseCommand
{
    public class BaseAnimalCommandValidator<T> : AbstractValidator<T> where T : BaseAnimalCommand
    {
        public static string NameRequired => "O nome é obrigatório.";
        public static string SpeciesRequired => "A espécie é obrigatória.";
        public static string BreedRequired => "A raça é obrigatória.";
        public static string SexRequired => "O sexo é obrigatório.";
        public static string CastratedRequired => "A informação de castração é obrigatória.";
        public static string ColorRequired => "A cor é obrigatória.";
        public static string SizeRequired => "O porte é obrigatório.";
        public static string AgeRequired => "A idade é obrigatória.";
        public static string DescriptionRequired => "A descrição é obrigatória.";
        public static string ObservationRequired => "A observação é obrigatória.";
        public static string PhotoRequired => "A foto é obrigatória.";
        public static string PixKeyMaxLength => "A chave Pix pode ter no máximo 100 caracteres.";

        public BaseAnimalCommandValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage(NameRequired)
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

            RuleFor(a => a.Especie)
                .NotEmpty().WithMessage(SpeciesRequired)
                .MaximumLength(50).WithMessage("A espécie pode ter no máximo 50 caracteres.");

            RuleFor(a => a.Raca)
                .NotEmpty().WithMessage(BreedRequired)
                .MaximumLength(50).WithMessage("A raça pode ter no máximo 50 caracteres.");

            RuleFor(a => a.Sexo)
                .NotNull().WithMessage(SexRequired);

            RuleFor(a => a.Castrado)
                .NotNull().WithMessage(CastratedRequired);

            RuleFor(a => a.Cor)
                .NotEmpty().WithMessage(ColorRequired)
                .MaximumLength(50).WithMessage("A cor pode ter no máximo 50 caracteres.");

            RuleFor(a => a.Porte)
                .NotEmpty().WithMessage(SizeRequired)
                .MaximumLength(50).WithMessage("O porte pode ter no máximo 50 caracteres.");

            RuleFor(a => a.Idade)
                .NotEmpty().WithMessage(AgeRequired)
                .MaximumLength(100).WithMessage("A idade pode ter no máximo 100 caracteres.");

            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage(DescriptionRequired)
                .MaximumLength(500).WithMessage("A descrição pode ter no máximo 500 caracteres.");

            RuleFor(a => a.Observacao)
                .NotEmpty().WithMessage(ObservationRequired)
                .MaximumLength(500).WithMessage("A observação pode ter no máximo 500 caracteres.");

            RuleFor(a => a.Foto)
                .NotEmpty().WithMessage(PhotoRequired);

            RuleFor(a => a.ChavePix)
                .MaximumLength(100).WithMessage(PixKeyMaxLength);
        }
    }
}