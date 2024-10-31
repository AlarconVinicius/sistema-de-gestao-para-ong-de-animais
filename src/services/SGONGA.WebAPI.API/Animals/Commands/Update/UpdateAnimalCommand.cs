using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Commands.Update;

public sealed record UpdateAnimalCommand : BaseAnimalCommand
{
    public UpdateAnimalCommand(
        Guid id,
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
        string ChavePix = "") : base(Nome, Especie, Raca, Sexo, Castrado, Cor, Porte, Idade, Descricao, Observacao, Foto, ChavePix)
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
        public const string IdRequired = "O Id é obrigatório.";
        public UpdateAnimalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}
