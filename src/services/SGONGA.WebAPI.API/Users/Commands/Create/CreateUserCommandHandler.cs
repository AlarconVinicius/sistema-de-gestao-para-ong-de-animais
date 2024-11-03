using SGONGA.Core.Extensions;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;
using SGONGA.WebAPI.Business.Users.Interfaces.Repositories;

namespace SGONGA.WebAPI.API.Users.Commands.Create;

public class CreateUserCommandHandler(IGenericUnitOfWork UnitOfWork, IUserRepository UserRepository, IIdentityHandler IdentityHandler) : ICommandHandler<CreateUserCommand>
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
            case EUsuarioTipo.Adotante:
                await UnitOfWork.InsertAsync(
                    Adotante.Create(
                        newUserId,
                        newTenantId,
                        command.Nome,
                        command.Apelido,
                        command.Documento,
                        command.Site,
                        new Contato(
                            command.Contato.Telefone,
                            command.Contato.Email),
                        command.TelefoneVisivel,
                        command.AssinarNewsletter,
                        command.DataNascimento,
                        command.Estado,
                        command.Cidade,
                        command.Sobre),
                    cancellationToken);
                break;

            case EUsuarioTipo.ONG:
                await UnitOfWork.InsertAsync(
                    ONG.Create(
                        newUserId,
                        newTenantId,
                        command.Nome,
                        command.Apelido,
                        command.Documento,
                        command.Site,
                        new Contato(
                            command.Contato.Telefone,
                            command.Contato.Email),
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
                return UsuarioErrors.NaoFoiPossivelCriarUsuario;
        }

        identityResult = await IdentityHandler.CreateAsync(new CreateUserRequest(newUserId, command.Contato.Email, command.Senha, command.ConfirmarSenha));
        if (identityResult.IsFailed)
            return identityResult.Errors;

        await IdentityHandler.AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(newUserId, new UserClaim("Tenant", newTenantId.ToString())));

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result> CheckAvailabilityAsync(CreateUserCommand command)
    {
        if ((await DocumentoDisponivel(command.Documento, command.UsuarioTipo)).IsFailed)
            return ValidationErrors.DocumentoEmUso(command.Documento);

        if ((await ApelidoDisponivel(command.Apelido, command.UsuarioTipo)).IsFailed)
            return ValidationErrors.ApelidoEmUso(command.Apelido);

        if ((await EmailDisponivel(command.Contato.Email, command.UsuarioTipo)).IsFailed)
            return ValidationErrors.EmailEmUso(command.Contato.Email);

        return Result.Ok();
    }

    private async Task<Result> ApelidoDisponivel(string apelido, EUsuarioTipo tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => (f.Apelido == apelido || f.Slug == apelido.SlugifyString()) && f.UsuarioTipo == tipo);

        return available ? Result.Ok() : ValidationErrors.ApelidoEmUso(apelido);
    }
    private async Task<Result> DocumentoDisponivel(string documento, EUsuarioTipo tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Documento == documento && f.UsuarioTipo == tipo);

        return available ? Result.Ok() : ValidationErrors.DocumentoEmUso(documento);
    }
    private async Task<Result> EmailDisponivel(string email, EUsuarioTipo tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Contato.Email.Endereco == email && f.UsuarioTipo == tipo);

        return available ? Result.Ok() : ValidationErrors.EmailEmUso(email);
    }
}
