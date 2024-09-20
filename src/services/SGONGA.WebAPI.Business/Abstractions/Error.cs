namespace SGONGA.WebAPI.Business.Abstractions;

public sealed record Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType errorType)
    {
        Code = code;
        Description = description;
        Type = errorType;
    }

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Valdiation(string code, string description) =>
        new(code, description, ErrorType.Valdiation);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("NULL_VALUE", "Null value was provided", ErrorType.Failure);
    public static readonly Error ConditionNotMet = new("CONDITION_NOT_MET", "The specified condition was not met.", ErrorType.Failure);
}
public enum ErrorType
{
    Failure = 0,
    Valdiation = 1,
    NotFound = 2,
    Conflict = 3
}