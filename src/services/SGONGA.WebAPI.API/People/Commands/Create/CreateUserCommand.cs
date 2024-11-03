using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Create;

public record CreateUserCommand(
    EUsuarioTipo UsuarioTipo,
    string Nome,
    string Apelido,
    string Documento,
    string Site,
    string Email,
    string Telefone,
    string Senha,
    string ConfirmarSenha,
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
        ValidationResult = new CreateUserCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class CreateUserCommandValidator : BaseUserCommandValidator<CreateUserCommand>
    {
        public const string PasswordRequired = "A senha é obrigatória.";
        public const string PasswordConfirmMismatch = "As senhas não conferem.";
        public const string MinMaxLength = "O campo deve ter entre {0} e {1} caracteres.";

        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage(PasswordRequired)
                .Length(6, 100).WithMessage(MinMaxLength);

            RuleFor(c => c.ConfirmarSenha)
                .Equal(c => c.Senha).WithMessage(PasswordConfirmMismatch);
        }
    }
}
