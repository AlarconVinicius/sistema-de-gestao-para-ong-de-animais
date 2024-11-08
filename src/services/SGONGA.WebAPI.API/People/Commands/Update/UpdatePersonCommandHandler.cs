using SGONGA.Core.User;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Users.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.People.Commands.Update;

internal sealed class UpdatePersonCommandHandler(IGenericUnitOfWork UnitOfWork, IPersonRepository PersonRepository, IIdentityHandler IdentityHandler, ITenantProvider TenantProvider, IAspNetUser AppUser) : ICommandHandler<UpdatePersonCommand>
{
    public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        if (AppUser.GetUserId() != request.Id)
            return PersonErrors.UsuarioNaoEncontrado(request.Id);

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        var personResult = await GetPersonByIdAsync(request.Id, tenantId.Value, request.PersonType, cancellationToken);
        if (personResult.IsFailed)
            return personResult.Errors;

        var person = personResult.Value;
        if (request.Email != person.Email.Address)
        {
            var emailIsAvailable = await IsEmailAvailable(request.Email, request.PersonType);
            if (emailIsAvailable.IsFailed)
                return emailIsAvailable.Errors;

            var identityEmailUpdated = await IdentityHandler.UpdateEmailAsync(new(request.Id, request.Email));
            if (identityEmailUpdated.IsFailed)
                return identityEmailUpdated.Errors;
        }
        UpdatePerson(person, request);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result<Person>> GetPersonByIdAsync(Guid personId, Guid tenantId, EPersonType personType, CancellationToken cancellationToken)
    {
        switch (personType)
        {
            case EPersonType.Adopter:
                var adopter = await PersonRepository.SearchAsync(q => q.Id == personId && q.TenantId == tenantId && q.PersonType == EPersonType.Adopter, cancellationToken) as Adopter;
                if (adopter is null)
                    return PersonErrors.UsuarioNaoEncontrado(personId);
                return adopter;
            case EPersonType.Organization:
                var organization = await PersonRepository.SearchAsync(q => q.Id == personId && q.TenantId == tenantId && q.PersonType == EPersonType.Organization, cancellationToken) as Organization;
                if (organization is null)
                    return PersonErrors.UsuarioNaoEncontrado(personId);
                return organization;
            default:
                return PersonErrors.NaoFoiPossivelAtualizarUsuario;
        };
    }
    private async Task<Result> IsEmailAvailable(string email, EPersonType type)
    {
        var available = !await PersonRepository.ExistsAsync(f => f.Email.Address == email && f.PersonType == type);

        return available ? Result.Ok() : PersonErrors.EmailEmUso(email);
    }
    private void UpdatePerson(Person person, UpdatePersonCommand request)
    {
        if (person is Organization organization && request.PersonType == EPersonType.Organization)
        {
            organization.Update(
                request.Name,
                request.Nickname,
                request.Site,
                request.PhoneNumber,
                request.Email,
                request.IsPhoneNumberVisible,
                request.State,
                request.City,
                request.About,
                request.PixKey
            );
            UnitOfWork.Update(organization);
        }
        else if (person is Adopter adopter && request.PersonType == EPersonType.Adopter)
        {
            adopter.Update(
                request.Name,
                request.Nickname,
                request.Site,
                request.PhoneNumber,
                request.Email,
                request.IsPhoneNumberVisible,
                request.State,
                request.City,
                request.About
            );
            UnitOfWork.Update(adopter);
        }
    }
}
