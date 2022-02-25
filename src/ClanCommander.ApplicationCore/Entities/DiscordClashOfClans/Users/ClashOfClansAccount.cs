namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users;

internal class ClashOfClansAccount : Entity, IAggregateRoot
{
    public PlayerId AccountId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public string Name { get; private set; }
    
    public ClashOfClansAccount(PlayerId accountId, string accountName)
    {
        Guard.Against.InvalidClashOfClansTag(accountId.Value, nameof(accountId));
        Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));

        AccountId = accountId;
        Name = accountName;
    }

    public void UpdateName(string accountName)
    {
        Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));

        Name = accountName;

        AddDomainEvent(new UserAccountNameUpdatedEvent());
    }
}
