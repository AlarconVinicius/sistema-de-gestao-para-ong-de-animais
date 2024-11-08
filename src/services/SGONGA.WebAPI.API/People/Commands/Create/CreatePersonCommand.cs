using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Create;
public record CreatePersonCommand(
    EPersonType PersonType,
    string Name,
    string Nickname,
    string Document,
    string Site,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword,
    bool IsPhoneNumberVisible,
    bool SubscribeToNewsletter,
    DateTime BirthDate,
    string State,
    string City,
    string? About,
    string? PixKey) : BasePersonCommand(PersonType, Name, Nickname, Document, Site, Email, PhoneNumber, IsPhoneNumberVisible, SubscribeToNewsletter, BirthDate, State, City, About, PixKey)
{
    public override Result IsValid()
    {
        ValidationResult = new CreatePersonCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class CreatePersonCommandValidator : BasePersonCommandValidator<CreatePersonCommand>
    {
        public const string PasswordRequired = "A senha é obrigatória.";
        public const string PasswordConfirmMismatch = "As senhas não conferem.";
        public const string MinMaxLength = "O campo deve ter entre {0} e {1} caracteres.";

        public CreatePersonCommandValidator()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(PasswordRequired)
                .Length(6, 100).WithMessage(MinMaxLength);

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password).WithMessage(PasswordConfirmMismatch);
        }
    }
}
