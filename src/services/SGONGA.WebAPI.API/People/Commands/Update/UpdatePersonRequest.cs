using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Update;

public sealed record UpdatePersonRequest(
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
        string? PixKey);