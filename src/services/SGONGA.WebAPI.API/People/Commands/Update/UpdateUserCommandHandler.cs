﻿using SGONGA.Core.User;
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

internal sealed class UpdateUserCommandHandler(IGenericUnitOfWork UnitOfWork, IPersonRepository UserRepository, IIdentityHandler IdentityHandler, ITenantProvider TenantProvider, IAspNetUser AppUser) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (AppUser.GetUserId() != request.Id)
            return PersonErrors.UsuarioNaoEncontrado(request.Id);

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        var personResult = await GetUserByIdAsync(request.Id, tenantId.Value, request.UsuarioTipo, cancellationToken);
        if (personResult.IsFailed)
            return personResult.Errors;

        var person = personResult.Value;
        if (request.Email != person.Email.Address)
        {
            var emailIsAvailable = await EmailDisponivel(request.Email, request.UsuarioTipo);
            if (emailIsAvailable.IsFailed)
                return emailIsAvailable.Errors;

            var identityEmailUpdated = await IdentityHandler.UpdateEmailAsync(new(request.Id, request.Email));
            if (identityEmailUpdated.IsFailed)
                return identityEmailUpdated.Errors;
        }
        UpdateUser(person, request);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result<Person>> GetUserByIdAsync(Guid userId, Guid tenantId, EUsuarioTipo usuarioTipo, CancellationToken cancellationToken)
    {
        switch (usuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                var adotante = await UserRepository.SearchAsync(q => q.Id == userId && q.TenantId == tenantId && q.UsuarioTipo == EUsuarioTipo.Adotante, cancellationToken) as Adopter;
                if (adotante is null)
                    return PersonErrors.UsuarioNaoEncontrado(userId);
                return adotante;
            case EUsuarioTipo.ONG:
                var ngo = await UserRepository.SearchAsync(q => q.Id == userId && q.TenantId == tenantId && q.UsuarioTipo == EUsuarioTipo.ONG, cancellationToken) as NGO;
                if (ngo is null)
                    return PersonErrors.UsuarioNaoEncontrado(userId);
                return ngo;
            default:
                return PersonErrors.NaoFoiPossivelAtualizarUsuario;
        };
    }
    private async Task<Result> EmailDisponivel(string email, EUsuarioTipo tipo)
    {
        var available = !await UserRepository.ExistsAsync(f => f.Email.Address == email && f.UsuarioTipo == tipo);

        return available ? Result.Ok() : PersonErrors.EmailEmUso(email);
    }
    private void UpdateUser(Person user, UpdateUserCommand request)
    {
        if (user is NGO ngo && request.UsuarioTipo == EUsuarioTipo.ONG)
        {
            ngo.Update(
                request.Nome,
                request.Apelido,
                request.Site,
                request.Telefone, 
                request.Email,
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
                request.Telefone,
                request.Email,
                request.TelefoneVisivel,
                request.Estado,
                request.Cidade,
                request.Sobre
            );
            UnitOfWork.Update(adopter);
        }
    }
}
