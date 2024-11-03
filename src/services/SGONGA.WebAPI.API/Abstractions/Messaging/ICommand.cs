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
public abstract record BaseCommand : ICommand
{
    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; } = new ValidationResult();

    public abstract Result IsValid();

    protected BaseCommand() { }
}
public abstract record BaseCommand<TResponse> : BaseCommand, ICommand<TResponse>
{
}