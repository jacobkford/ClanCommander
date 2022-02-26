﻿namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;

internal class GuildClanMember : Entity
{
    public PlayerId MemberId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public ClanMemberRole ClanRole { get; private set; }

#pragma warning disable CS8618
    // For EF Core
    private GuildClanMember() { }
#pragma warning restore CS8618

    public GuildClanMember(PlayerId memberId, DiscordUserId userId, ClanMemberRole clanRole)
    {
        MemberId = memberId;
        UserId = userId;
        ClanRole = clanRole;
    }

    public void Promote()
    {
        var newRole = ClanRole.Promote();

        if (newRole is null)
        {
            throw new InvalidOperationException();
        }
        else
        {
            ClanRole = newRole;
        }
    }

    public void Demote()
    {
        var newRole = ClanRole.Demote();

        if (newRole is null)
        {
            throw new InvalidOperationException();
        }
        else
        {
            ClanRole = newRole;
        }
    }
}
