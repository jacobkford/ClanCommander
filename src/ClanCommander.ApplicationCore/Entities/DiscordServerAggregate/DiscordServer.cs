namespace ClanCommander.ApplicationCore.Entities.DiscordServerAggregate;

internal class DiscordServer : Entity, IAggregateRoot
{
    public DiscordServerId Id { get; private set; }

    public string Name { get; private set; }

    public string? Prefix { get; private set; }

    public bool MessageCommandsEnabled { get; private set; }

    private DiscordServer() { }

    public DiscordServer(DiscordServerId id, string name)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));

        Id = id;
        Name = name;
        Prefix = null;
        MessageCommandsEnabled = false;
    }

    public ClanAggregate.Clan CreateServerClan(ClanId clanId, string clanName)
    {
        return new ClanAggregate.Clan(clanId, clanName, Id);
    }

    public void UpdatePrefix(string prefix)
    {
        Prefix = Guard.Against.NullOrWhiteSpace(prefix, nameof(prefix));
    }

    public void EnableMessageCommands()
    {
        Guard.Against.NullOrWhiteSpace(Prefix, nameof(Prefix));
        MessageCommandsEnabled = true;
    }

    public void DisableMessageCommands()
    {
        MessageCommandsEnabled = false;
    }
}

