using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Errors;

public static class AnimalErrors
{
    public static Error AnimalNotFound(Guid id) => Error.NotFound("ANIMAL_NOT_FOUND", $"O animal com Id = '{id}' não foi encontrado.");
    public static readonly Error NoAnimalsFound = Error.NotFound("NO_ANIMALS_FOUND", "Nenhum animal encontrado.");
}