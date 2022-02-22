namespace ClanCommander.ApplicationCore.Entities.Guild;

using ClanCommander.ApplicationCore.Entities.Discord.GuildClashOfClans.Clans;

internal class RegisteredDiscordGuild : Entity, IAggregateRoot
{
    public DiscordGuildId ServerId { get; private set; }

    public string Name { get; private set; }

    public DiscordUserId OwnerId { get; private set; }

    private RegisteredDiscordGuild() { }

    public RegisteredDiscordGuild(DiscordGuildId id, string name, DiscordUserId ownerId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.InvalidDiscordSnowflakeId(ownerId.Value, nameof(ownerId));

        ServerId = id;
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
    public static GuildClan CreateClashOfClansClan(this RegisteredDiscordGuild instance, ClashOfClansClanId clanId, string clanName)
    {
        return new GuildClan(clanId, clanName, instance.ServerId);
    }
}

