using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.Business.Abstractions;

public class Result
{
    public bool IsFailed => Errors.Any();

    public bool IsSuccess => !IsFailed;

    public List<Error> Errors { get; } = [];

    protected Result() { }

    protected Result(IEnumerable<Error> errors)
    {
        if (errors == null || !errors.Any())
            throw new ArgumentException("A lista de erros não pode ser nula ou vazia para uma falha.");

        Errors.AddRange(errors);
    }

    public static Result Ok() => new();

    public static Result Fail(Error error)
    {
        if (error == null)
            throw new ArgumentNullException(nameof(error), "O erro não pode ser nulo.");

        return new Result([error]);
    }

    public static Result Fail(IEnumerable<Error> errors)
    {
        if (errors == null || !errors.Any())
            throw new ArgumentNullException(nameof(errors), "A lista de erros não pode ser nula.");

        return new Result(errors);
    }

    public static Result<TValue> Ok<TValue>(TValue value) => new(value);

    public static Result<TValue> Fail<TValue>(Error error) => new([error]);

    public static Result<TValue> Fail<TValue>(IEnumerable<Error> errors) => new(errors);


    public static implicit operator Result(Error error) => Fail(error);

    public static implicit operator Result(List<Error> errors) => Fail(errors);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue value)
    {
        _value = value;
    }

    protected internal Result(IEnumerable<Error> errors)
        : base(errors) { }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("O valor de um resultado de falha não pode ser acessado.");

    public static implicit operator Result<TValue>(TValue value) => Ok(value);

    public static implicit operator Result<TValue>(Error error) => Fail<TValue>(error);

    public static implicit operator Result<TValue>(List<Error> errors) => Fail<TValue>(errors);
}
public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}