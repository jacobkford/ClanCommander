namespace ClanCommander.ApplicationCore.Extensions;

public static class DiscordSnowflakeExtensions
{
    public static DateTimeOffset? ConvertDiscordSnowflakeToDateTime(this ulong snowflake)
    {
        var discordEpoch = 1420070400000u;
        return DateTimeOffset.FromUnixTimeMilliseconds((long)((snowflake / 4194304) + discordEpoch));
    }
}
