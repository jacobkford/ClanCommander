namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;

internal class GuildClan : Entity, IAggregateRoot
{
    public ClanId ClanId { get; private set; }

    public string Name { get; private set; }

    public DiscordGuildId GuildId { get; private set; }

    public ulong DiscordRoleId { get; private set; }

    public IReadOnlyCollection<GuildClanMember> Members => _members.AsReadOnly();
    private readonly List<GuildClanMember> _members = new List<GuildClanMember>();

#pragma warning disable CS8618
    // For EF Core
    private GuildClan() { }
#pragma warning restore CS8618

    public GuildClan(ClanId clanId, string clanName, DiscordGuildId guildId)
    {
        Guard.Against.InvalidClashOfClansTag(clanId.Value, nameof(clanId));
        Guard.Against.NullOrEmpty(clanName, nameof(clanName));
        Guard.Against.InvalidDiscordSnowflakeId(guildId.Value, nameof(guildId));

        ClanId = clanId;
        Name = clanName;
        GuildId = guildId;

        this.AddDomainEvent(new DiscordGuildClanAddedEvent(clanId, clanName, guildId));
    }

    public void ChangeDiscordRole(ulong newRoleId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(newRoleId, nameof(newRoleId));

        var oldRoleId = DiscordRoleId;
        DiscordRoleId = newRoleId;

        this.AddDomainEvent(new DiscordGuildClanIdentifierRoleChangedEvent(ClanId, Name, GuildId, oldRoleId, newRoleId));
    }

    public void DisableDiscordRole()
    {
        var oldRoleId = DiscordRoleId;
        DiscordRoleId = default;

        this.AddDomainEvent(new DiscordGuildClanIdentifierRoleChangedEvent(ClanId, Name, GuildId, oldRoleId, default));
    }

    public void AddClanMember(PlayerId memberId, DiscordUserId userId)
    {
        Guard.Against.InvalidClashOfClansTag(memberId.Value, nameof(memberId));
        Guard.Against.InvalidDiscordSnowflakeId(userId.Value, nameof(userId));

        var memberAlreadyExists = _members.Any(m => m.MemberId == memberId);

        if (memberAlreadyExists)
            throw new ArgumentException($"{memberId} has already been added");

        var member = new GuildClanMember(memberId, userId);
        _members.Add(member);

        this.AddDomainEvent(new DiscordGuildClanMemberAddedEvent(ClanId, Name, GuildId, memberId, userId));
    }

    public void RemoveClanMember(PlayerId memberId)
    {
        var member = _members.SingleOrDefault(member => member.MemberId == memberId);

        Guard.Against.Null(member, nameof(member), "Couldn't find registered clan member with the provided identity");

        var userId = member.UserId;

        _members.Remove(member);

        this.AddDomainEvent(new DiscordGuildClanMemberRemovedEvent(ClanId, Name, GuildId, memberId, userId));
    }
}

internal static class GuildClanExtensions
{
    public static bool HasClanIdentiferDiscordRole(this GuildClan instance)
    {
        return instance.DiscordRoleId != default;
    }
}