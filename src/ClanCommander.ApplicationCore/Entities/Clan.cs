namespace ClanCommander.ApplicationCore.Entities;

internal class Clan : Entity, IAggregateRoot
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public ulong DiscordServerId { get; private set; }

    private readonly List<ClanMember> _members = new List<ClanMember>();
    public IReadOnlyCollection<ClanMember> Members => _members.AsReadOnly();

    private Clan() { }

    public Clan(string id, string name, ulong discordServerId)
    {
        Id = Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        DiscordServerId = Guard.Against.InvalidDiscordSnowflakeId(discordServerId, nameof(discordServerId));
    }

    public void AddClanMember(string memberId, ulong userId)
    {
        var memberAlreadyExists = _members.Any(m => m.Id == memberId);

        if (memberAlreadyExists)
            throw new ArgumentException($"{memberId} has already been added");

        var member = new ClanMember(memberId, userId);
        _members.Add(member);
    }
}
