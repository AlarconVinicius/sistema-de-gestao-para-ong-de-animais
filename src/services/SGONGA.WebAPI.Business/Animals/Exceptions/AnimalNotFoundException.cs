using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.Animals.Exceptions;

public sealed class AnimalNotFoundException : NotFoundException
{
    public AnimalNotFoundException(Guid animalId)
        : base($"Animal com ID = {animalId.ToString()} não encontrado")
    {
    }
}