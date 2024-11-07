using System.Text.RegularExpressions;

namespace SGONGA.Core.Utils;

public static class RegexUtils
{
    #region TimeSpan
    public static readonly Regex TimeSpanWithSecondsRegex = new(
        @"^(?:[01]\d|2[0-3]):(?:[0-5]\d):(?:[0-5]\d)$",
        RegexOptions.Compiled
    );

    public static readonly Regex TimeSpanWithoutSecondsRegex = new(
        @"^(?:[01]\d|2[0-3]):(?:[0-5]\d)$",
        RegexOptions.Compiled
    );
    #endregion

    #region DateTime
    public static readonly Regex DateTimeWithHourRegex = new(
        @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4} (?:[01]\d|2[0123]):(?:[012345]\d)$",
        RegexOptions.Compiled
    );

    public static readonly Regex DateTimeWithoutHourRegex = new(
        @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$",
        RegexOptions.Compiled
    );
    #endregion

    #region PhoneNumber
    public static readonly Regex CharacterPhoneRegex = new(
        @"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})",
        RegexOptions.Compiled
    );
    public static readonly Regex PhoneRegex = new(
        @"^(\d{9}|\d{11})$",
        RegexOptions.Compiled
    );
    #endregion

    #region Email
    public static readonly Regex EmailRegex = new(
        @"^(?("")("".+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]\.)+[a-z]{2,6}))$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );
    #endregion
}