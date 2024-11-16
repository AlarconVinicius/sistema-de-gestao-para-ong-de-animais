using FluentValidation.Results;
using MediatR;
using SGONGA.WebAPI.Business.Abstractions;
using System.Text.Json.Serialization;

namespace SGONGA.WebAPI.API.Abstractions.Messaging;

public interface ICommand :IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
public abstract record Command
{
    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; } = new ValidationResult();

    public abstract Result IsValid();

    protected Command() { }
}
public abstract record BaseCommand : Command, ICommand
{
}
public abstract record BaseCommand<TResponse> : Command, ICommand<TResponse>
{
}