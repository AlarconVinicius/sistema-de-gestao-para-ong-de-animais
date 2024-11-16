using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.Animals.Exceptions;

public sealed class AnimalValidationException : BadRequestException
{
    public AnimalValidationException(Error[] errors)
        : base("ANIMAL_VALIDATION_FAILED", errors)
    {
    }
    public AnimalValidationException(Error errors)
        : base("ANIMAL_VALIDATION_FAILED", errors)
    {
    }
}
