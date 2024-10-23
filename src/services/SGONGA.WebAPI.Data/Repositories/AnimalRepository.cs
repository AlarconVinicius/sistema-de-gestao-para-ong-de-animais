using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;
using System.Linq.Expressions;

namespace SGONGA.WebAPI.Data.Repositories;

internal sealed class AnimalRepository(ONGDbContext db) : Repository<Animal>(db), IAnimalRepository
{
    public async Task<Animal> SearchAsync(Expression<Func<Animal, bool>> predicate, CancellationToken cancellationToken = default) => 
        await DbSet
            .Where(predicate)
            .FirstOrDefaultAsync(cancellationToken) ?? null!;

    public async Task<AnimalResponse> GetByIdAsync(
        Guid id, 
        Guid? tenantId = null, 
        CancellationToken cancellationToken = default)
    {
        var queryable = DbSet
            .Include(q => q.ONG)
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
                a.Nome,
                a.Especie,
                a.Raca,
                a.Sexo,
                a.Castrado,
                a.Cor,
                a.Porte,
                a.Idade,
                a.ONG.Nome,
                $"{a.ONG.Estado}, {a.ONG.Cidade}",
                a.Descricao,
                a.Observacao,
                a.ChavePix,
                a.Foto,
                a.CreatedAt,
                a.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken) ?? null;

        return animal!;
    }

    public async Task<BasePagedResponse<AnimalResponse>> GetAllAsync(
        int page = 1,
        int pageSize = 10,
        string? query = null,
        Guid? tenantId = null,
        bool returnAll = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = DbSet.Include(q => q.ONG)
            .AsNoTracking()
            .AsQueryable();

        if (tenantId.HasValue)
        {
            queryable = queryable.Where(q => q.TenantId == tenantId);
        }
        if (!string.IsNullOrEmpty(query))
        {
            queryable = queryable.Where(q => q.Nome.Contains(query) || q.ONG!.Nome.Contains(query));
        }

        queryable = queryable.OrderByDescending(q => q.UpdatedAt)
            .ThenBy(q => q.Nome);

        var animalResponses = queryable.Select(a => new AnimalResponse(
            a.Id,
            a.TenantId,
            a.Nome,
            a.Especie,
            a.Raca,
            a.Sexo,
            a.Castrado,
            a.Cor,
            a.Porte,
            a.Idade,
            a.ONG.Nome,
            $"{a.ONG.Estado}, {a.ONG.Cidade}",
            a.Descricao,
            a.Observacao,
            a.ChavePix,
            a.Foto,
            a.CreatedAt,
            a.UpdatedAt
        ));
        var totalResults = await queryable.CountAsync(cancellationToken);
        if (!returnAll)
        {
            animalResponses = animalResponses.Skip((page - 1) * pageSize).Take(pageSize);
        }
        var list = await animalResponses.ToListAsync(cancellationToken);

        return new BasePagedResponse<AnimalResponse>()
        {
            List = list,
            TotalResults = totalResults,
            PageIndex = page,
            PageSize = pageSize
        };
    }
}