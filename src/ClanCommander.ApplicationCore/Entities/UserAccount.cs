namespace ClanCommander.ApplicationCore.Entities;

public class UserAccount : Entity
{
    public string Id { get; private set; }

    public string Name { get; private set; }
    
    public UserAccount(string accountId, string accountName)
    {
        Id = Guard.Against.InvalidClashOfClansTag(accountId, nameof(accountId));
        Name = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
    }

    public void UpdateName(string accountName)
    {
        Name = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
    }
}
