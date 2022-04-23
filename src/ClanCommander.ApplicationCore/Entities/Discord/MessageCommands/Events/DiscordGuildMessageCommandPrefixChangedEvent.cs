namespace ClanCommander.ApplicationCore.Entities.Discord.MessageCommands.Events;

internal class DiscordGuildMessageCommandPrefixChangedEvent : DomainEvent
{
    public DiscordGuildId GuildId { get; private set; }

    public string OldPrefix { get; private set; }

    public string NewPrefix { get; private set; }

    public DiscordGuildMessageCommandPrefixChangedEvent(DiscordGuildId guildId, string oldPrefix, string newPrefix)
    {
        GuildId = guildId;
        OldPrefix = oldPrefix;
        NewPrefix = newPrefix;
    }
}
