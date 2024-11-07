using SGONGA.WebAPI.Business.Shared.Exceptions;

namespace SGONGA.WebAPI.Business.People.Exceptions;

public sealed class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid userid)
        : base($"Pessoa com ID = {userid.ToString()} não encontrada")
    {
    }
}