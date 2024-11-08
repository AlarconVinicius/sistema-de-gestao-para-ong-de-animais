using FluentValidation;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Update;

public sealed record UpdatePersonCommand(
    Guid Id,
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
    string? PixKey) : BasePersonCommand(PersonType, Name, Nickname, Document, Site, Email, PhoneNumber, IsPhoneNumberVisible, SubscribeToNewsletter, BirthDate, State, City, About, PixKey)
{
    public override Result IsValid()
    {
        ValidationResult = new UpdatePersonCommandValidator().Validate(this);
        if (!ValidationResult.IsValid)
        {
            return ValidationResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();
        }

        return Result.Ok();
    }

    public class UpdatePersonCommandValidator : BasePersonCommandValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(IdRequired);
        }
    }

}
