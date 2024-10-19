using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Animals.Errors;

public static class AnimalErrors
{
    public static Error AnimalNotFound(Guid id) => Error.NotFound("ANIMAL_NOT_FOUND", $"The animal with Id = '{id}' was not found.");
    public static readonly Error NoAnimalsFound = Error.NotFound("NO_ANIMALS_FOUND", "No animals found.");
}