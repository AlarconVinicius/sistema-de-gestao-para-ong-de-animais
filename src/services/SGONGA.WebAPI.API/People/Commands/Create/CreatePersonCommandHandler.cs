using SGONGA.Core.Extensions;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Users.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Users.Requests;
using SGONGA.WebAPI.Business.Users.Responses;

namespace SGONGA.WebAPI.API.People.Commands.Create;

public class CreatePersonCommandHandler(IGenericUnitOfWork UnitOfWork, IPersonRepository PersonRepository, IIdentityHandler IdentityHandler) : ICommandHandler<CreatePersonCommand>
{
    public async Task<Result> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {

        Result validationResult = command.IsValid();
        if (validationResult.IsFailed)
            return validationResult.Errors;

        Result available = await CheckAvailabilityAsync(command);
        if (available.IsFailed)
            return available.Errors;

        Result identityResult;
        Guid newPersonId = Guid.NewGuid();
        Guid newTenantId = Guid.NewGuid();
        switch (command.PersonType)
        {
            case EPersonType.Adopter:
                await UnitOfWork.InsertAsync(
                    Adopter.Create(
                        newPersonId,
                        newTenantId,
                        command.Name,
                        command.Nickname,
                        command.Document,
                        command.Site,
                        command.PhoneNumber,
                        command.Email,
                        command.IsPhoneNumberVisible,
                        command.SubscribeToNewsletter,
                        command.BirthDate,
                        command.State,
                        command.City,
                        command.About),
                    cancellationToken);
                break;

            case EPersonType.Organization:
                await UnitOfWork.InsertAsync(
                    Organization.Create(
                        newPersonId,
                        newTenantId,
                        command.Name,
                        command.Nickname,
                        command.Document,
                        command.Site,
                        command.PhoneNumber,
                        command.Email,
                        command.IsPhoneNumberVisible,
                        command.SubscribeToNewsletter,
                        command.BirthDate,
                        command.State,
                        command.City,
                        command.About,
                        command.PixKey),
                    cancellationToken);
                break;

            default:
                return PersonErrors.NaoFoiPossivelCriarUsuario;
        }

        identityResult = await IdentityHandler.CreateAsync(new CreateUserRequest(newPersonId, command.Email, command.Password, command.ConfirmPassword));
        if (identityResult.IsFailed)
            return identityResult.Errors;

        await IdentityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(newPersonId, new UserClaim("Tenant", newTenantId.ToString())));

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result> CheckAvailabilityAsync(CreatePersonCommand command)
    {
        if ((await DocumentAvailable(command.Document, command.PersonType)).IsFailed)
            return PersonErrors.DocumentoEmUso(command.Document);

        if ((await NicknameAvailable(command.Nickname, command.PersonType)).IsFailed)
            return PersonErrors.ApelidoEmUso(command.Nickname);

        if ((await EmailAvailable(command.Email)).IsFailed)
            return PersonErrors.EmailEmUso(command.Email);

        return Result.Ok();
    }

    private async Task<Result> NicknameAvailable(string nickname, EPersonType type)
    {
        var available = !await PersonRepository.ExistsAsync(f => (f.Nickname == nickname || f.Slug == nickname.SlugifyString()) && f.PersonType == type);

        return available ? Result.Ok() : PersonErrors.ApelidoEmUso(nickname);
    }

    private async Task<Result> DocumentAvailable(string document, EPersonType type)
    {
        var available = !await PersonRepository.ExistsAsync(f => f.Document == document && f.PersonType == type);

        return available ? Result.Ok() : PersonErrors.DocumentoEmUso(document);
    }

    private async Task<Result> EmailAvailable(string email)
    {
        var available = !await PersonRepository.ExistsAsync(f => f.Email.Address == email);

        return available ? Result.Ok() : PersonErrors.EmailEmUso(email);
    }
}
