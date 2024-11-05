using FluentValidation;
using SGONGA.Core.Utils;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.People.Enum;
using SiteValueObject = SGONGA.WebAPI.Business.People.ValueObjects.Site;

namespace SGONGA.WebAPI.API.People.Commands;

public abstract record BaseUserCommand(
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
        string? ChavePix) : BaseCommand
{
    public class BaseUserCommandValidator<T> : AbstractValidator<T> where T : BaseUserCommand
    {

        public const string NameRequired = "O nome é obrigatório.";
        public const string NicknameRequired = "O apelido é obrigatório.";
        public const string DocumentRequired = "O documento é obrigatório.";
        public const string SiteRequired = "O site é obrigatório.";
        public const string BirthdateRequired = "A data de nascimento é obrigatória.";
        public const string StateRequired = "O estado é obrigatório.";
        public const string CityRequired = "A cidade é obrigatória.";
        public const string MaxLengthExceeded = "O campo deve ter no máximo {0} caracteres.";
        public const string InvalidUrl = "URL inválida.";
        public const string UserTypeRequired = "O tipo de usuário é obrigatório.";
        public const string ContactRequired = "As informações de contato são obrigatórias.";

        public BaseUserCommandValidator()
        {
            RuleFor(c => c.UsuarioTipo)
                .IsInEnum().WithMessage(UserTypeRequired);

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage(NameRequired)
                .MaximumLength(100).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Apelido)
                .NotEmpty().WithMessage(NicknameRequired)
                .MaximumLength(100).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage(DocumentRequired)
                .MaximumLength(11).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Site)
                .NotEmpty().WithMessage(SiteRequired)
                .MaximumLength(500).WithMessage(MaxLengthExceeded)
                .Must(BeAValidUrl).WithMessage(InvalidUrl);

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage(BirthdateRequired);

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage(StateRequired)
                .MaximumLength(50).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage(CityRequired)
                .MaximumLength(50).WithMessage(MaxLengthExceeded);
        }

        private bool BeAValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;
            return RegexUtils.SiteRegex.IsMatch(url);
        }
    }
}