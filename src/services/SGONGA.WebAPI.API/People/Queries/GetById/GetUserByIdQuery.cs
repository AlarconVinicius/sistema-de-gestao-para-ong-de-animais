using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.People.Responses;

namespace SGONGA.WebAPI.API.People.Queries.GetById;

public record GetUserByIdQuery(Guid Id, bool TenantFiltro = false) : IQuery<PersonResponse>;