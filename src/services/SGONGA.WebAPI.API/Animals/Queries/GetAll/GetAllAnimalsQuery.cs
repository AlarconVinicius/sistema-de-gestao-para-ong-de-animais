﻿using SGONGA.WebAPI.API.Shared;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Shared.Responses;

namespace SGONGA.WebAPI.API.Animals.Queries.GetAll;

public class GetAllAnimalsQuery : BasePagedQuery<BasePagedResponse<AnimalResponse>>
{
    public bool TenantFilter { get; set; }
    public GetAllAnimalsQuery() { }

    public GetAllAnimalsQuery(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFilter = false) : base(pageSize, pageNumber, query, returnAll)
    {
        TenantFilter = tenantFilter;
    }
}