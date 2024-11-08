using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.API.People.Commands;

using SiteValueObject = Site;
using DocumentValueObject = Document;

public abstract record BasePersonCommand(
        EPersonType PersonType,
        string Name,
        string Nickname,
        string Document,
        string Site,
        string Email,
        string PhoneNumber,
        bool IsPhoneNumberVisible,
        bool SubscribeToNewsletter,
        DateTime BirthDate,
        string State,
        string City,
        string? About,
        string? PixKey) : BaseCommand
{
    public class BasePersonCommandValidator<T> : AbstractValidator<T> where T : BasePersonCommand
    {
        public const string IdRequired = "O Id é obrigatório.";
        public const string NameRequired = "O nome é obrigatório.";
        public const string NicknameRequired = "O apelido é obrigatório.";
        public const string DocumentRequired = "O documento é obrigatório.";
        public const string DocumentInvalid = "O documento informado é inválido.";
        public const string SiteRequired = "O site é obrigatório.";
        public const string BirthdateRequired = "A data de nascimento é obrigatória.";
        public const string StateRequired = "O estado é obrigatório.";
        public const string CityRequired = "A cidade é obrigatória.";
        public const string MaxLengthExceeded = "O campo deve ter no máximo {0} caracteres.";
        public const string InvalidUrl = "URL inválida.";
        public const string UserTypeRequired = "O tipo de usuário é obrigatório.";
        public const string AboutMaxLength = "A sobre pode ter no máximo 500 caracteres.";

        public BasePersonCommandValidator()
        {
            RuleFor(c => c.PersonType)
                .IsInEnum().WithMessage(UserTypeRequired);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(NameRequired)
                .MaximumLength(100).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Nickname)
                .NotEmpty().WithMessage(NicknameRequired)
                .MaximumLength(100).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.Document)
                .NotEmpty().WithMessage(DocumentRequired)
                .Must(BeAValidDocument).WithMessage(DocumentInvalid);

            RuleFor(c => c.Site)
                .NotEmpty().WithMessage(SiteRequired)
                .MaximumLength(500).WithMessage(MaxLengthExceeded)
                .Must(BeAValidUrl).WithMessage(InvalidUrl);

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage(BirthdateRequired);

            RuleFor(c => c.State)
                .NotEmpty().WithMessage(StateRequired)
                .MaximumLength(50).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.City)
                .NotEmpty().WithMessage(CityRequired)
                .MaximumLength(50).WithMessage(MaxLengthExceeded);

            RuleFor(c => c.About)
                .MaximumLength(500).WithMessage(AboutMaxLength);
        }
        private bool BeAValidDocument(string document)
        {
            try
            {
                return DocumentValueObject.Validate(document);
            }
            catch
            {
                return false;
            }
        }
        private bool BeAValidUrl(string url)
        {
            return SiteValueObject.IsValidUrl(url);
        }
    }
}