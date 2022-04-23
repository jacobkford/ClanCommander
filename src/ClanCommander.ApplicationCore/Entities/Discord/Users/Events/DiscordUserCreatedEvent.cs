namespace ClanCommander.ApplicationCore.Entities.Discord.Users.Events;

internal class DiscordUserCreatedEvent : DomainEvent
{
    public DiscordUserId UserId { get; private set; }

    public string Username { get; private set; }

    public DiscordUserCreatedEvent(DiscordUserId userId, string username)
    {
        UserId = userId;
        Username = username;
    }
}
