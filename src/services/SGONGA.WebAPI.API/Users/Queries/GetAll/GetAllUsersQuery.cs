using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Users.Responses;

namespace SGONGA.WebAPI.API.Users.Queries.GetAll;

public class GetAllUsersQuery : BasePagedQuery<BasePagedResponse<PersonResponse>>
{
    public EUsuarioTipo UsuarioTipo { get; }
    public bool TenantFiltro { get; }
    public GetAllUsersQuery() { }

    public GetAllUsersQuery(EUsuarioTipo usuarioTipo, int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFiltro = false) : base(pageSize, pageNumber, query, returnAll)
    {
        UsuarioTipo = usuarioTipo;
        TenantFiltro = tenantFiltro;
    }
}