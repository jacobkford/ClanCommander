namespace ClanCommander.ApplicationCore.Entities.ClanAggregate;

internal class ClanMember : Entity
{
    public string Id { get; private set; }

    public UserId UserId { get; private set; }

    public bool CWLParticipant { get; private set; }

    public ClanMember(string id, UserId userId)
    {
        Guard.Against.InvalidClashOfClansTag(id, nameof(id));
        Guard.Against.InvalidDiscordSnowflakeId(userId.Value, nameof(userId));

        Id = id;
        UserId = userId;
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
