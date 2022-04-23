namespace ClanCommander.ApplicationCore.Entities.Discord.Guilds.Events;

internal class DiscordGuildNameChangedEvent : DomainEvent
{
    public DiscordGuildId GuildId { get; private set; }

    public string BeforeName { get; private set; }

    public string AfterName { get; private set; }

    public DiscordGuildNameChangedEvent(DiscordGuildId guildId, string beforeName, string afterName)
    {
        GuildId = guildId;
        BeforeName = beforeName;
        AfterName = afterName;
    }
}
