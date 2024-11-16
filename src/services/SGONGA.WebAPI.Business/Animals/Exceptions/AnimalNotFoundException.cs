using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.Animals.Exceptions;

public sealed class AnimalNotFoundException : NotFoundException
{
    public AnimalNotFoundException(Guid animalId)
        : base($"The animal with Id = '{animalId.ToString()}' was not found.")
    {
    }
}