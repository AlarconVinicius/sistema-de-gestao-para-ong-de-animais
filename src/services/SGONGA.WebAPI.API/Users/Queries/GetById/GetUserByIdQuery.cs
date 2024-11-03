
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Users.Responses;

namespace SGONGA.WebAPI.API.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id, bool TenantFiltro = false) : IQuery<PersonResponse>;