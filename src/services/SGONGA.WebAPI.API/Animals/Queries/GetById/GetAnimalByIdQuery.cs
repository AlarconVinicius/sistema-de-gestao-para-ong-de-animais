using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Animals.Responses;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

public record GetAnimalByIdQuery(Guid Id, bool TenantFilter = false) : IQuery<AnimalResponse>;
