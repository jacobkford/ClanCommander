namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Users;

internal class ClashOfClansAccount : Entity, IAggregateRoot
{
    public PlayerId AccountId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public string Name { get; private set; }

#pragma warning disable CS8618
    // For EF Core
    private ClashOfClansAccount() { }
#pragma warning restore CS8618

    public ClashOfClansAccount(PlayerId accountId, DiscordUserId userId, string accountName)
    {
        Guard.Against.InvalidClashOfClansTag(accountId.Value, nameof(accountId));
        Guard.Against.InvalidDiscordSnowflakeId(userId.Value, nameof(userId));
        Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));

        AccountId = accountId;
        UserId = userId;
        Name = accountName;

        this.AddDomainEvent(new DiscordUserLinkedClashOfClansAccountEvent(accountId, userId, accountName));
    }

    public void UpdateName(string accountName)
    {
        Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
        Name = accountName;
    }
}
