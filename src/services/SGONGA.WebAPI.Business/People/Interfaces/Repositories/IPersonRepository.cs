﻿using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Responses;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Responses;

namespace SGONGA.WebAPI.Business.People.Interfaces.Repositories;
public interface IPersonRepository : IRepository<Person>
{
    Task<NGO> GetNGOByIdWithAnimalsAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);

    Task<Result<NGO>> GetBySlugAsync(string slug, Guid? tenantId, CancellationToken cancellationToken = default);

    Task<Result<EPersonType>> IdentifyUserType(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<PersonResponse> GetAdopterByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<PersonResponse> GetNGOByIdAsync(Guid id, Guid? tenantId, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<PersonResponse>> GetAllAdoptersPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
    Task<BasePagedResponse<PersonResponse>> GetAllNGOsPagedAsync(Guid? tenantId, int page = 1, int pageSize = 10, string? query = null, bool returnAll = false, CancellationToken cancellationToken = default);
}
