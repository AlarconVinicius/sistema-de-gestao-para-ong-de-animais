using MediatR;
using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.API.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}