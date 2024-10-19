using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Animals.Queries.GetById;

public record GetAnimalByIdQuery(Guid Id, bool TenantFiltro = false) : IQuery<AnimalResponse>;
