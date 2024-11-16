using FluentValidation;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Identity.Responses;

namespace SGONGA.WebAPI.API.Users.Commands.Login;

using EmailValueObject = Business.People.ValueObjects.Email;
public record LoginUserCommand(string Email, string Password) : BaseCommand<LoginUserResponse>
{
    public override Result IsValid()
    {
        ValidationResult = new LoginUserCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public const string PasswordRequired = "The password is required.";
        public const string EmailInvalid = "The email address provided is invalid.";
        
        public LoginUserCommandValidator()
        {
            RuleFor(c => c.Email)
                .Must(BeAValidEmail).WithMessage(EmailInvalid);

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(PasswordRequired);
        }

        private bool BeAValidEmail(string email)
        {
            try
            {
                return EmailValueObject.IsValidEmail(email);
            }
            catch
            {
                return false;
            }
        }
    }
}
