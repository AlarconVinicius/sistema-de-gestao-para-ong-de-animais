using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;

namespace SGONGA.WebAPI.Business.Models;

internal sealed class TenantProvider(IONGDbContext Context) : ITenantProvider
{
    private Guid TenantId = Guid.Empty;

    public async Task<Result<Guid>> GetTenantId()
    {
        if (TenantId == Guid.Empty)
            return Error.NotFound("TENANT_ID_NOT_FOUND", "O Tenant ID não foi encontrado. Certifique-se de que o cabeçalho 'TenantId' está presente na requisição e tente novamente.");
        
        return await Context.ONGs.AnyAsync(q => q.TenantId == TenantId)
            ? TenantId
            : Error.NotFound("INVALID_TENANT_ID", "TenantId não encontrado.");
    }

    public Result SetTenantId(IHeaderDictionary headerDictionary)
    {
        if (!headerDictionary.TryGetValue("TenantId", out StringValues tenantIdValues) || tenantIdValues.Count == 0)
        {
            return Error.NotFound("INVALID_TENANT_ID", "Cabeçalho 'TenantId' não encontrado ou vazio.");
        }

        if (!Guid.TryParse(tenantIdValues[0], out Guid parsedTenantId))
        {
            return Error.NotFound("INVALID_TENANT_ID_FORMAT", "O formato do Tenant ID é inválido.");
        }

        TenantId = parsedTenantId;

        return Result.Ok();
    }
    public async Task<bool> IsTenantValid(Guid tenantId)
    {
        return await Context.ONGs.AnyAsync(q => q.TenantId == tenantId);
    }
}
