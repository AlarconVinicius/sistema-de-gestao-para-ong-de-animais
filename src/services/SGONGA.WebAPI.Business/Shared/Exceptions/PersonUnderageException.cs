namespace SGONGA.WebAPI.Business.Shared.Exceptions;

public class PersonUnderageException : DomainException
{
    public PersonUnderageException(string message) : base(message) { }
}