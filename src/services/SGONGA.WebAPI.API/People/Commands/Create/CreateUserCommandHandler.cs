using SGONGA.Core.Extensions;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Responses;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Users.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Users.Requests;

namespace SGONGA.WebAPI.API.People.Commands.Create;

public class CreateUserCommandHandler(IGenericUnitOfWork UnitOfWork, IPersonRepository UserRepository, IIdentityHandler IdentityHandler) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {

        Result validationResult = command.IsValid();
        if (validationResult.IsFailed)
            return validationResult.Errors;

        Result available = await CheckAvailabilityAsync(command);
        if (available.IsFailed)
            return available.Errors;

        Result identityResult;
        Guid newUserId = Guid.NewGuid();
        Guid newTenantId = Guid.NewGuid();
        switch (command.UsuarioTipo)
        {
            case EPersonType.Adopter:
                await UnitOfWork.InsertAsync(
                    Adopter.Create(
                        newUserId,
                        newTenantId,
                        command.Nome,
                        command.Apelido,
                        command.Documento,
                        command.Site,
                        command.Telefone,
                        command.Email,
                        command.TelefoneVisivel,
                        command.AssinarNewsletter,
                        command.DataNascimento,
                        command.Estado,
                        command.Cidade,
                        command.Sobre),
                    cancellationToken);
                break;

            case EPersonType.NGO:
                await UnitOfWork.InsertAsync(
                    NGO.Create(
                        newUserId,
                        newTenantId,
                        command.Nome,
                        command.Apelido,
                        command.Documento,
                        command.Site,
                        command.Telefone,
                        command.Email,
                        command.TelefoneVisivel,
                        command.AssinarNewsletter,
                        command.DataNascimento,
                        command.Estado,
                        command.Cidade,
                        command.Sobre,
                        command.ChavePix),
                    cancellationToken);
                break;

            default:
                return PersonErrors.NaoFoiPossivelCriarUsuario;
        }

        identityResult = await IdentityHandler.CreateAsync(new CreateUserRequest(newUserId, command.Email, command.Senha, command.ConfirmarSenha));
        if (identityResult.IsFailed)
            return identityResult.Errors;

        await IdentityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(newUserId, new UserClaim("Tenant", newTenantId.ToString())));

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result> CheckAvailabilityAsync(CreateUserCommand command)
    {
        if ((await DocumentoDisponivel(command.Documento, command.UsuarioTipo)).IsFailed)
            return PersonErrors.DocumentoEmUso(command.Documento);

        if ((await ApelidoDisponivel(command.Apelido, command.UsuarioTipo)).IsFailed)
            return PersonErrors.ApelidoEmUso(command.Apelido);

        if ((await EmailDisponivel(command.Email)).IsFailed)
            return PersonErrors.EmailEmUso(command.Email);

        return Result.Ok();
    }

    private async Task<Result> ApelidoDisponivel(string apelido, EPersonType tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => (f.Nickname == apelido || f.Slug == apelido.SlugifyString()) && f.UserType == tipo);

        return available ? Result.Ok() : PersonErrors.ApelidoEmUso(apelido);
    }
    private async Task<Result> DocumentoDisponivel(string documento, EPersonType tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Document == documento && f.UserType == tipo);

        return available ? Result.Ok() : PersonErrors.DocumentoEmUso(documento);
    }
    private async Task<Result> EmailDisponivel(string email)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Email.Address == email);

        return available ? Result.Ok() : PersonErrors.EmailEmUso(email);
    }
}
