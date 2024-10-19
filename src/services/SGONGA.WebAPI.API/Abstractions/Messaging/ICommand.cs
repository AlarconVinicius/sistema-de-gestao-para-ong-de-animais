using MediatR;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Abstractions.Messaging;

public interface ICommand :IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
