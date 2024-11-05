using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class PersonRepository(ONGDbContext db) : Repository<Person>(db), IPersonRepository
{
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

        return PersonErrors.UsuarioNaoEncontrado(id);
    }
    public async Task<NGO> GetNGOByIdWithAnimalsAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<NGO>()
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
    public async Task<Result<NGO>> GetBySlugAsync(string slug, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<NGO>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        return await queryable.Where(q => q.Slug == slug)
                              .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public async Task<PersonResponse> GetAdopterByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Adopter>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        return await queryable.Where(q => q.Id == id)
            .Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            q.Telefone.Number,
            q.Email.Address,
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

    public async Task<PersonResponse> GetNGOByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<NGO>()
                          .AsNoTracking()
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        return await queryable.Where(q => q.Id == id)
            .Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            q.Telefone.Number,
            q.Email.Address,
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

    public async Task<BasePagedResponse<PersonResponse>> GetAllAdoptersPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Adopter>().AsNoTracking().AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);
        var responses = queryable.Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            q.Telefone.Number,
            q.Email.Address,
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

        return new BasePagedResponse<PersonResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
    public async Task<BasePagedResponse<PersonResponse>> GetAllNGOsPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<NGO>().AsNoTracking().AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);
        var responses = queryable.Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Nome,
            q.Apelido,
            q.UsuarioTipo,
            q.Documento,
            q.Site,
            q.Telefone.Number,
            q.Email.Address,
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

        return new BasePagedResponse<PersonResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
}
