using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.People.Exceptions;

public sealed class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid personId)
        : base($"Pessoa com ID = {personId.ToString()} não encontrada")
    {
    }
}