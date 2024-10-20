using Microsoft.AspNetCore.Http;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Interfaces.Services;
public interface ITenantProvider
{
    public Result<Guid> GetTenantId();
    public Result SetTenantId(IHeaderDictionary headerDictionary);
}
