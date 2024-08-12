namespace SGONGA.Core.Extensions;
public static class StringExtensions
{
    public static Guid? TryParseGuid(this string? input)
    {
        if (Guid.TryParse(input, out Guid guid))
        {
            return guid;
        }
        return null;
    }
}
