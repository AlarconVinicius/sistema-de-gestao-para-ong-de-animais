using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;

namespace SGONGA.WebAPI.API.People.Queries.GetAll;

public class GetAllPeopleQuery : BasePagedQuery<BasePagedResponse<PersonResponse>>
{
    public EPersonType PersonType { get; }
    public bool TenantFilter { get; }
    public GetAllPeopleQuery() { }

    public GetAllPeopleQuery(EPersonType usuarioTipo, int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFiltro = false) : base(pageSize, pageNumber, query, returnAll)
    {
        PersonType = usuarioTipo;
        TenantFilter = tenantFiltro;
    }
}