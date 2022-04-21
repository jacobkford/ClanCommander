namespace ClanCommander.ApplicationCore.Interfaces;

public interface IDiscordUserService
{
    Task<string?> GetUsername(ulong userId);
    bool IsBotOwner(ulong userId);
    Task<bool> IsGuildOwner(ulong guildId, ulong userId);
}