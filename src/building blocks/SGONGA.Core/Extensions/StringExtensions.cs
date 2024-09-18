using Slugify;

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

    public static string SlugifyString(this string input)
    {
        SlugHelper helper = new();
        return helper.GenerateSlug(input);
    }
}
