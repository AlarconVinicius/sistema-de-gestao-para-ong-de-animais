using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Errors;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class PersonRepository(OrganizationDbContext db) : Repository<Person>(db), IPersonRepository
{
    public async Task<Result<EPersonType>> IdentifyPersonType(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var tipos = (EPersonType[])Enum.GetValues(typeof(EPersonType));

        foreach (var tipo in tipos)
        {
            bool exists = await DbSet.AnyAsync(q => q.Id == id &&
                                                (tenantId == null || q.TenantId == tenantId) &&
                                                q.PersonType == tipo, cancellationToken);
            if (exists)
                return tipo;
        }

        return PersonErrors.UsuarioNaoEncontrado(id);
    }
    public async Task<Organization> GetOrganizationByIdWithAnimalsAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Organization>()
                          .AsNoTracking()
                          .Include(c => c.Animals)
                          .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        return await queryable.Where(q => q.Id == id)
                              .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }
    public async Task<Result<Organization>> GetBySlugAsync(string slug, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Organization>()
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
            q.Name,
            q.Nickname,
            q.PersonType,
            q.Document,
            q.Site,
            q.PhoneNumber.Number,
            q.Email.Address,
            q.IsPhoneNumberVisible,
            q.SubscribeToNewsletter,
            q.BirthDate,
            q.State,
            q.City,
            q.About,
            null,
            q.CreatedAt,
            q.UpdatedAt
        ))
        .FirstOrDefaultAsync(cancellationToken) ?? null!;
    }

    public async Task<PersonResponse> GetOrganizationByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Organization>()
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
            q.Name,
            q.Nickname,
            q.PersonType,
            q.Document,
            q.Site,
            q.PhoneNumber.Number,
            q.Email.Address,
            q.IsPhoneNumberVisible,
            q.SubscribeToNewsletter,
            q.BirthDate,
            q.State,
            q.City,
            q.About,
            q.PixKey,
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
            .ThenBy(q => q.Name);
        var responses = queryable.Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Name,
            q.Nickname,
            q.PersonType,
            q.Document,
            q.Site,
            q.PhoneNumber.Number,
            q.Email.Address,
            q.IsPhoneNumberVisible,
            q.SubscribeToNewsletter,
            q.BirthDate,
            q.State,
            q.City,
            q.About,
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
    public async Task<BasePagedResponse<PersonResponse>> GetAllOrganizationsPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default)
    {
        var queryable = Db.Set<Organization>().AsNoTracking().AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Name);
        var responses = queryable.Select(q => new PersonResponse(
            q.Id,
            q.TenantId,
            q.Name,
            q.Nickname,
            q.PersonType,
            q.Document,
            q.Site,
            q.PhoneNumber.Number,
            q.Email.Address,
            q.IsPhoneNumberVisible,
            q.SubscribeToNewsletter,
            q.BirthDate,
            q.State,
            q.City,
            q.About,
            q.PixKey,
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
