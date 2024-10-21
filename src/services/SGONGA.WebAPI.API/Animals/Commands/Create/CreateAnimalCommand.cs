using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Commands.Create;

public sealed record CreateAnimalCommand(
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
    string ChavePix = "") : BaseAnimalCommand(Nome, Especie, Raca, Sexo, Castrado, Cor, Porte, Idade, Descricao, Observacao, Foto, ChavePix)
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