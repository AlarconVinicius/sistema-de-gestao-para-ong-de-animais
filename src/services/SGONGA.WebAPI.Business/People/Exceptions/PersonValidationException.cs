using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.People.Exceptions;

public sealed class PersonValidationException : BadRequestException
{
    public PersonValidationException(Error[] errors)
        : base("FALHA_AO_VALIDAR_PESSOA", errors)
    {
    }
    public PersonValidationException(Error errors)
        : base("FALHA_AO_VALIDAR_PESSOA", errors)
    {
    }
}
