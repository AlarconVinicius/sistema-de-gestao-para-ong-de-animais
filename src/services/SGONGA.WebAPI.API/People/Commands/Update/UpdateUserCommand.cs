using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Update;

public sealed record UpdateUserCommand(
        Guid Id,
        EUsuarioTipo UsuarioTipo,
        string Nome,
        string Apelido,
        string Documento,
        string Site,
        string Email,
        string Telefone,
        bool TelefoneVisivel,
        bool AssinarNewsletter,
        DateTime DataNascimento,
        string Estado,
        string Cidade,
        string? Sobre,
        string? ChavePix) : BaseUserCommand(UsuarioTipo, Nome, Apelido, Documento, Site, Email, Telefone, TelefoneVisivel, AssinarNewsletter, DataNascimento, Estado, Cidade, Sobre, ChavePix)
{
    public override Result IsValid()
    {
        ValidationResult = new UpdateUserCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class UpdateUserCommandValidator : BaseUserCommandValidator<UpdateUserCommand>
    {
        public const string IdRequired = "O Id é obrigatório.";
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}
