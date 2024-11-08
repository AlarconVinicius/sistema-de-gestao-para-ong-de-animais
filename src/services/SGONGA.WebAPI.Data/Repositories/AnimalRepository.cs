using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

internal sealed class AnimalRepository(ONGDbContext db) : Repository<Animal>(db), IAnimalRepository
{
    public async Task<AnimalResponse> GetByIdAsync(
        Guid id, 
        Guid? tenantId = null, 
        CancellationToken cancellationToken = default)
    {
        var queryable = DbSet
            .Include(q => q.Ngo)
            .AsNoTracking();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }

        var animal = await queryable
            .Where(a => a.Id == id)
            .Select(a => new AnimalResponse(
                a.Id,
                a.TenantId,
                a.Name,
                a.Species,
                a.Breed,
                a.Gender,
                a.Neutered,
                a.Color,
                a.Size,
                a.Age,
                a.Ngo.Name,
                a.Ngo.State, 
                a.Ngo.City,
                a.Description,
                a.Note,
                a.PixKey,
                a.Photo,
                a.CreatedAt,
                a.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken) ?? null;

        return animal!;
    }

    public async Task<BasePagedResponse<AnimalResponse>> GetAllAsync(
        Guid? tenantId,
        int page = 1,
        int pageSize = 10,
        string? query = null,
        bool returnAll = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = DbSet.Include(q => q.Ngo)
            .AsNoTracking()
            .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        if (!string.IsNullOrEmpty(query))
        {
            queryable = queryable.Where(q => q.Name.Contains(query) || q.Ngo!.Name.Contains(query));
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Name);

        var animalResponse = queryable.Select(a => new AnimalResponse(
            a.Id,
            a.TenantId,
            a.Name,
            a.Species,
            a.Breed,
            a.Gender,
            a.Neutered,
            a.Color,
            a.Size,
            a.Age,
            a.Ngo.Name,
            a.Ngo.State,
            a.Ngo.City,
            a.Description,
            a.Note,
            a.PixKey,
            a.Photo,
            a.CreatedAt,
            a.UpdatedAt
        ));
        var totalResults = await queryable.CountAsync(cancellationToken);
        if (!returnAll)
        {
            animalResponse = animalResponse.Skip((page - 1) * pageSize).Take(pageSize);
        }
        var list = await animalResponse.ToListAsync(cancellationToken);

        return new BasePagedResponse<AnimalResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
}