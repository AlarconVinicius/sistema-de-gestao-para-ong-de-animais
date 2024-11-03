using SGONGA.Core.User;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;

namespace SGONGA.WebAPI.API.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler(IGenericUnitOfWork UnitOfWork, IUserRepository UserRepository, IIdentityHandler IdentityHandler, ITenantProvider TenantProvider, IAspNetUser AppUser) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (AppUser.GetUserId() != request.Id)
            return UsuarioErrors.UsuarioNaoEncontrado(request.Id);

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        var personResult = await GetUserByIdAsync(request.Id, tenantId.Value, request.UsuarioTipo, cancellationToken);
        if(personResult.IsFailed)
            return personResult.Errors;

        var person = personResult.Value;
        if (request.Contato.Email != person.Contato.Email.Endereco)
        {
            var emailIsAvailable = await EmailDisponivel(request.Contato.Email, request.UsuarioTipo);
            if (emailIsAvailable.IsFailed)
                return emailIsAvailable.Errors;

            var identityEmailUpdated = await IdentityHandler.UpdateEmailAsync(new(request.Id, request.Contato.Email));
            if (identityEmailUpdated.IsFailed)
                return identityEmailUpdated.Errors;
        }
        UpdateUser(person, request);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result<Person>> GetUserByIdAsync(Guid userId, Guid tenantId, EUsuarioTipo usuarioTipo, CancellationToken cancellationToken)
    {
        switch(usuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                var adotante = await UserRepository.SearchAsync(q => q.Id == userId && q.TenantId == tenantId && q.UsuarioTipo == EUsuarioTipo.Adotante, cancellationToken) as Adopter;
                if (adotante is null)
                    return UsuarioErrors.UsuarioNaoEncontrado(userId);
                return adotante;
            case EUsuarioTipo.ONG:
                var ngo = await UserRepository.SearchAsync(q => q.Id == userId && q.TenantId == tenantId && q.UsuarioTipo == EUsuarioTipo.ONG, cancellationToken) as NGO;
                if(ngo is null)
                    return UsuarioErrors.UsuarioNaoEncontrado(userId);
                return ngo;
            default:
                return UsuarioErrors.NaoFoiPossivelAtualizarUsuario;
        };
    }
    private async Task<Result> EmailDisponivel(string email, EUsuarioTipo tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Contato.Email.Endereco == email && f.UsuarioTipo == tipo);

        return available ? Result.Ok() : ValidationErrors.EmailEmUso(email);
    }
    private void UpdateUser(Person user, UpdateUserCommand request)
    {
        if (user is NGO ngo && request.UsuarioTipo == EUsuarioTipo.ONG)
        {
            ngo.Update(
                request.Nome,
                request.Apelido,
                request.Site,
                new(request.Contato.Telefone, request.Contato.Email),
                request.TelefoneVisivel,
                request.Estado,
                request.Cidade,
                request.Sobre,
                request.ChavePix
            );
            UnitOfWork.Update(ngo);
        }
        else if (user is Adopter adopter && request.UsuarioTipo == EUsuarioTipo.Adotante)
        {
            adopter.Update(
                request.Nome,
                request.Apelido,
                request.Site,
                new(request.Contato.Telefone, request.Contato.Email),
                request.TelefoneVisivel,
                request.Estado,
                request.Cidade,
                request.Sobre
            );
            UnitOfWork.Update(adopter);
        }
    }
}
