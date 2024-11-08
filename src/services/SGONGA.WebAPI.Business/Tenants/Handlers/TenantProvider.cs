using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.Business.Tenants.Handlers;

internal sealed class TenantProvider(IPersonRepository PersonRepository) : ITenantProvider
{
    private Guid TenantId = Guid.Empty;

    public async Task<Result<Guid>> GetTenantId()
    {
        if (TenantId == Guid.Empty)
            return Error.NotFound("TENANT_ID_NOT_FOUND", "O Tenant ID não foi encontrado. Certifique-se de que o cabeçalho 'TenantId' está presente na requisição e tente novamente.");

        return await PersonRepository.ExistsAsync(q => q.TenantId == TenantId && q.PersonType == EPersonType.Organization)
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
}
