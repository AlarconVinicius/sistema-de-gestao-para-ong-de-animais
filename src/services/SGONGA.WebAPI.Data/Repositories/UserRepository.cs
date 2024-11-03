﻿using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Errors;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;
using SGONGA.WebAPI.Business.Users.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Users.Responses;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

public class UserRepository : Repository<Usuario>, IUserRepository
{
    public UserRepository(ONGDbContext db) : base(db)
    {
    }

    public async Task<Usuario> SearchAsync(Expression<Func<Usuario, bool>> predicate, CancellationToken cancellationToken = default) =>
        await DbSet
            .Where(predicate)
            .FirstOrDefaultAsync(cancellationToken) ?? null!;
    public async Task<Result<EUsuarioTipo>> IdentifyUserType(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var tipos = (EUsuarioTipo[])Enum.GetValues(typeof(EUsuarioTipo));

        foreach (var tipo in tipos)
        {
            bool exists = await DbSet.AnyAsync(q => q.Id == id &&
                                                (tenantId == null || q.TenantId == tenantId) &&
                                                q.UsuarioTipo == tipo, cancellationToken);
            if (exists)
                return tipo;
        }

        return UsuarioErrors.UsuarioNaoEncontrado(id);
    }
    public async Task<ONG> GetNGOByIdWithAnimalsAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<ONG>()
                          .AsNoTracking()
                          .Include(c => c.Animais)
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        return await queryable.Where(q => q.Id == id)
                              .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }
    public async Task<Result<ONG>> GetBySlugAsync(string slug, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<ONG>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        return await queryable.Where(q => q.Slug == slug)
                              .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public async Task<UserResponse> GetAdopterByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Adotante>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        return await queryable.Where(q => q.Id == id)
            .Select(q => new UserResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            new ContatoResponse(
                q.Contato.Telefone.Numero,
                q.Contato.Email.Endereco),
            q.TelefoneVisivel,
            q.AssinarNewsletter,
            q.DataNascimento,
            q.Estado,
            q.Cidade,
            q.Sobre,
            null,
            q.CreatedAt,
            q.UpdatedAt
        ))
        .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public async Task<UserResponse> GetNGOByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<ONG>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        return await queryable.Where(q => q.Id == id)
            .Select(q => new UserResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            new ContatoResponse(
                q.Contato.Telefone.Numero,
                q.Contato.Email.Endereco),
            q.TelefoneVisivel,
            q.AssinarNewsletter,
            q.DataNascimento,
            q.Estado,
            q.Cidade,
            q.Sobre,
            q.ChavePix,
            q.CreatedAt,
            q.UpdatedAt
        ))
        .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public async Task<BasePagedResponse<UserResponse>> GetAllAdoptersPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Adotante>().AsNoTracking().AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);
        var responses = queryable.Select(q => new UserResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            new ContatoResponse(
                q.Contato.Telefone.Numero,
                q.Contato.Email.Endereco),
            q.TelefoneVisivel,
            q.AssinarNewsletter,
            q.DataNascimento,
            q.Estado,
            q.Cidade,
            q.Sobre,
            null,
            q.CreatedAt,
            q.UpdatedAt
        ));
        var totalResults = await queryable.CountAsync(cancellationToken);
        if (!returnAll)
        {
            responses = responses.Skip((page - 1) * pageSize).Take(pageSize);
        }
        var list = await responses.ToListAsync(cancellationToken);

        return new BasePagedResponse<UserResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
    public async Task<BasePagedResponse<UserResponse>> GetAllNGOsPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<ONG>().AsNoTracking().AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);
        var responses = queryable.Select(q => new UserResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            new ContatoResponse(
                q.Contato.Telefone.Numero,
                q.Contato.Email.Endereco),
            q.TelefoneVisivel,
            q.AssinarNewsletter,
            q.DataNascimento,
            q.Estado,
            q.Cidade,
            q.Sobre,
            q.ChavePix,
            q.CreatedAt,
            q.UpdatedAt
        ));
        var totalResults = await queryable.CountAsync(cancellationToken);
        if (!returnAll)
        {
            responses = responses.Skip((page - 1) * pageSize).Take(pageSize);
        }
        var list = await responses.ToListAsync(cancellationToken);

        return new BasePagedResponse<UserResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
}