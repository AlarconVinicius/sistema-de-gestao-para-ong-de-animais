using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Shared.Exceptions;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message, Error[]? errors = null)
        : base(message)
    {
        Errors = errors ?? [];
    }
    protected BadRequestException(string message, Error? error = null)
        : base(message)
    {
        Errors = error == null ? [] : [error] ;
    }
    public Error[] Errors { get; }
}
