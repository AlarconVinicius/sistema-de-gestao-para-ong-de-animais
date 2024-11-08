using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.Animals.Exceptions;

public sealed class AnimalValidationException : BadRequestException
{
    public AnimalValidationException(Error[] errors)
        : base("FALHA_AO_VALIDAR_ANIMAL", errors)
    {
    }
    public AnimalValidationException(Error errors)
        : base("FALHA_AO_VALIDAR_ANIMAL", errors)
    {
    }
}
