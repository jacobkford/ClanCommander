namespace ClanCommander.ApplicationCore.Entities.Discord.Users;

internal class UserClashOfClansAccount : Entity
{
    public ClashOfClansPlayerId AccountId { get; private set; }

    public string Name { get; private set; }
    
    public UserClashOfClansAccount(ClashOfClansPlayerId accountId, string accountName)
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
