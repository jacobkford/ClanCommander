namespace ClanCommander.ApplicationCore.Interfaces;

public interface IMessageCommandService
{
    Task<string?> GetGuildPrefixAsync(ulong? guildId);
}