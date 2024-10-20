using FluentValidation;

namespace SGONGA.WebAPI.API.Animals.Command;

public class BaseAnimalCommandValidator : AbstractValidator<BaseAnimalCommand>
{
    public BaseAnimalCommandValidator()
    {
        RuleFor(a => a.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

        RuleFor(a => a.Especie)
            .NotEmpty().WithMessage("A espécie é obrigatória.")
            .MaximumLength(50).WithMessage("A espécie pode ter no máximo 50 caracteres.");

        RuleFor(a => a.Raca)
            .NotEmpty().WithMessage("A raça é obrigatória.")
            .MaximumLength(50).WithMessage("A raça pode ter no máximo 50 caracteres.");

        RuleFor(a => a.Sexo)
            .NotNull().WithMessage("O sexo é obrigatório.");

        RuleFor(a => a.Castrado)
            .NotNull().WithMessage("A informação de castração é obrigatória.");

        RuleFor(a => a.Cor)
            .NotEmpty().WithMessage("A cor é obrigatória.")
            .MaximumLength(50).WithMessage("A cor pode ter no máximo 50 caracteres.");

        RuleFor(a => a.Porte)
            .NotEmpty().WithMessage("O porte é obrigatório.")
            .MaximumLength(50).WithMessage("O porte pode ter no máximo 50 caracteres.");

        RuleFor(a => a.Idade)
            .NotEmpty().WithMessage("A idade é obrigatória.")
            .MaximumLength(100).WithMessage("A idade pode ter no máximo 100 caracteres.");

        RuleFor(a => a.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(500).WithMessage("A descrição pode ter no máximo 500 caracteres.");

        RuleFor(a => a.Observacao)
            .NotEmpty().WithMessage("A observação é obrigatória.")
            .MaximumLength(500).WithMessage("A observação pode ter no máximo 500 caracteres.");

        RuleFor(a => a.Foto)
            .NotEmpty().WithMessage("A foto é obrigatória.");

        RuleFor(a => a.ChavePix)
            .MaximumLength(100).WithMessage("A chave Pix pode ter no máximo 100 caracteres.");
    }
}