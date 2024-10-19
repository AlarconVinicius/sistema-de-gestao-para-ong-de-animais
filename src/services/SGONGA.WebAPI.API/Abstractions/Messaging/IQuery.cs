using MediatR;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}