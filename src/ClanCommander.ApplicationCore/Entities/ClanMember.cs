namespace ClanCommander.ApplicationCore.Entities;

internal class ClanMember : Entity
{
    public string Id { get; private set; }

    public ulong UserId { get; private set; }

    public bool CWLParticipant { get; private set; }

    public ClanMember(string id, ulong userId)
    {
        Id = Guard.Against.NullOrWhiteSpace(id, nameof(id));
        UserId = Guard.Against.InvalidDiscordSnowflakeId(userId, nameof(userId));
        CWLParticipant = false;
    }

    public void OptInCWL()
    {
        CWLParticipant = true;
    }

    public void OptOutCWL()
    {
        CWLParticipant = false;
    }
}
