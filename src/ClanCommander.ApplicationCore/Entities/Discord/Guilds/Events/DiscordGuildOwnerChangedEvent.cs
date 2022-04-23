namespace ClanCommander.ApplicationCore.Entities.Discord.Guilds.Events;

internal class DiscordGuildOwnerChangedEvent : DomainEvent
{
    public DiscordGuildId GuildId { get; private set; }

    public DiscordUserId OldOwner { get; private set; }

    public DiscordUserId NewOwner { get; private set; }

    public DiscordGuildOwnerChangedEvent(DiscordGuildId guildId, DiscordUserId oldOwner, DiscordUserId newOwner)
    {
        GuildId = guildId;
        OldOwner = oldOwner;
        NewOwner = newOwner;
    }
}
