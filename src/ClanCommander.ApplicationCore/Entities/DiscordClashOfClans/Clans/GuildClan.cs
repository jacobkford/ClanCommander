namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;

internal class GuildClan : Entity, IAggregateRoot
{
    public ClanId ClanId { get; private set; }

    public string Name { get; private set; }

    public DiscordGuildId GuildId { get; private set; }

    public ulong DiscordRoleId { get; private set; }

    public IReadOnlyCollection<GuildClanMember> Members => _members.AsReadOnly();
    private readonly List<GuildClanMember> _members = new List<GuildClanMember>();

    public GuildClan(ClanId clanId, string clanName, DiscordGuildId guildId)
    {
        Guard.Against.InvalidClashOfClansTag(clanId.Value, nameof(clanId));
        Guard.Against.NullOrEmpty(clanName, nameof(clanName));
        Guard.Against.InvalidDiscordSnowflakeId(guildId.Value, nameof(guildId));

        ClanId = clanId;
        Name = clanName;
        GuildId = guildId;
    }

    public void ChangeDiscordRole(ulong role)
    {
        Guard.Against.InvalidDiscordSnowflakeId(role, nameof(role));

        DiscordRoleId = role;
    }

    public void DisableDiscordRole()
    {
        DiscordRoleId = default;
    }

    public void AddClanMember(PlayerId memberId, DiscordUserId userId, ClanMemberRole role)
    {
        Guard.Against.InvalidClashOfClansTag(memberId.Value, nameof(memberId));
        Guard.Against.InvalidDiscordSnowflakeId(userId.Value, nameof(userId));

        var memberAlreadyExists = _members.Any(m => m.MemberId == memberId);

        if (memberAlreadyExists)
            throw new ArgumentException($"{memberId} has already been added");

        var member = new GuildClanMember(memberId, userId, role);
        _members.Add(member);
    }

    public void RemoveClanMember(PlayerId memberId)
    {
        var member = _members.SingleOrDefault(member => member.MemberId == memberId);

        Guard.Against.Null(member, nameof(member), "Couldn't find registered clan member with the provided identity");

        _members.Remove(member);
    }
}

internal static class GuildClanExtensions
{
    public static bool HasClanIdentiferDiscordRole(this GuildClan instance)
    {
        return instance.DiscordRoleId != default;
    }
}