namespace ClanCommander.ApplicationCore.Entities.Discord.Guilds.Events;

internal class DiscordGuildRegisteredEvent : DomainEvent
{
    public DiscordGuildId GuildId { get; private set; }

    public string GuildName { get; private set; }

    public DiscordGuildRegisteredEvent(DiscordGuildId guildId, string guildName)
    {
        GuildId = guildId;
        GuildName = guildName;
    }
}
