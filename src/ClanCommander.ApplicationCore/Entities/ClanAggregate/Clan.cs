namespace ClanCommander.ApplicationCore.Entities.ClanAggregate;

internal class Clan : Entity, IAggregateRoot
{
    public ClanId Id { get; private set; }

    public string Name { get; private set; }

    public DiscordServerId DiscordServerId { get; private set; }

    private readonly List<ClanMember> _members = new List<ClanMember>();
    public IReadOnlyCollection<ClanMember> Members => _members.AsReadOnly();

    private Clan() { }

    public Clan(ClanId id, string name, DiscordServerId discordServerId)
    {
        Guard.Against.InvalidClashOfClansTag(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.InvalidDiscordSnowflakeId(discordServerId.Value, nameof(discordServerId));

        Id = id;
        Name = name;
        DiscordServerId = discordServerId;
    }

    public void AddClanMember(string memberId, UserId userId)
    {
        var memberAlreadyExists = _members.Any(m => m.Id == memberId);

        if (memberAlreadyExists)
            throw new ArgumentException($"{memberId} has already been added");

        var member = new ClanMember(memberId, userId);
        _members.Add(member);
    }
}
