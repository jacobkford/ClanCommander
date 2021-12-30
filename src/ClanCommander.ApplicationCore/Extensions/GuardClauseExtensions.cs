namespace Ardalis.GuardClauses;

public static class GuardClauseExtensions
{
    public static ulong InvalidDiscordSnowflakeId(this IGuardClause guardClause, ulong input, string parameterName)
    {
        if (input < 4194304)
            throw new ArgumentException("Invalid input, snowflakes are much larger numbers", parameterName);

        var snowflakeDateTime = input.ConvertDiscordSnowflakeToDateTime();

        Guard.Against.Null(snowflakeDateTime, nameof(snowflakeDateTime));

        return input;
    }

    public static string InvalidClashOfClansTag(this IGuardClause guardClause, string input, string parameterName, string? message = null)
    {
        Guard.Against.NullOrWhiteSpace(input, parameterName, message);

        if (!Regex.Match(input, RegexConstants.ClashOfClansTag).Success)
        {
            throw new ArgumentException(message ?? "Invalid tag", parameterName);
        }
        return input;
    }
}
