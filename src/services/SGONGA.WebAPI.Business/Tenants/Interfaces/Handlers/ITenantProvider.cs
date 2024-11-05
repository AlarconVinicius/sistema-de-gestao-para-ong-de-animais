using Microsoft.AspNetCore.Http;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;
public interface ITenantProvider
{
    Task<Result<Guid>> GetTenantId();
    Result SetTenantId(IHeaderDictionary headerDictionary);
}
