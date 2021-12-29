namespace ClanCommander.ApplicationCore.Entities;

internal class DiscordServer : Entity, IAggregateRoot
{
    public ulong Id { get; private set; }

    public string Name { get; private set; }

    public string? Prefix { get; private set; }

    public bool MessageCommandsEnabled { get; private set; }

    private DiscordServer() { }

    public DiscordServer(ulong id, string name)
    {
        Id = Guard.Against.InvalidDiscordSnowflakeId(id, nameof(id));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Prefix = null;
        MessageCommandsEnabled = false;
    }

    public Clan CreateServerClan(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));

        return new Clan(id, name, Id);
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

