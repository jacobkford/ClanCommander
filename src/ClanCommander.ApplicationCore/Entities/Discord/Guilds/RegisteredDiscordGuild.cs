namespace ClanCommander.ApplicationCore.Entities.Discord.Guilds;

internal class RegisteredDiscordGuild : Entity, IAggregateRoot
{
    public DiscordGuildId GuildId { get; private set; }

    public string Name { get; private set; }

    public DiscordUserId OwnerId { get; private set; }

#pragma warning disable CS8618
    // For EF Core
    private RegisteredDiscordGuild() { }
#pragma warning restore CS8618 

    public RegisteredDiscordGuild(DiscordGuildId id, string name, DiscordUserId ownerId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.InvalidDiscordSnowflakeId(ownerId.Value, nameof(ownerId));

        GuildId = id;
        Name = name;
        OwnerId = ownerId;

        AddDomainEvent(new DiscordServerRegisteredEvent());
    }

    public void UpdateGuildName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));

        Name = name;
    }

    public void ChangeOwner(DiscordUserId newOwnerId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(newOwnerId.Value, nameof(newOwnerId));

        OwnerId = newOwnerId;
    }
}

internal static class RegisteredDiscordGuildExtensions
{
    public static GuildClan CreateClashOfClansClan(this RegisteredDiscordGuild instance, ClanId clanId, string clanName)
    {
        return new GuildClan(clanId, clanName, instance.GuildId);
    }
}

