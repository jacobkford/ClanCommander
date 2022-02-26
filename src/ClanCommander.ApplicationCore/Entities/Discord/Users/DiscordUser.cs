namespace ClanCommander.ApplicationCore.Entities.Discord.Users;

internal class DiscordUser : Entity, IAggregateRoot
{
    public DiscordUserId UserId { get; private set; }

    public string Username { get; private set; }

#pragma warning disable CS8618
    // For EF Core
    private DiscordUser() { }
#pragma warning restore CS8618

    public DiscordUser(DiscordUserId id, string discordUsername)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(discordUsername, nameof(discordUsername));

        UserId = id;
        Username = discordUsername;

        AddDomainEvent(new UserCreatedEvent());
    }
}
