namespace ClanCommander.ApplicationCore.Entities.UserAggregate;

internal class User : Entity, IAggregateRoot
{
    public UserId Id { get; private set; }

    public string DiscordUsername { get; private set; }

    private readonly List<UserAccount> _accounts = new List<UserAccount>();
    public IReadOnlyCollection<UserAccount> Accounts => _accounts.AsReadOnly();

    private User() { }

    public User(UserId id, string discordUsername)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(discordUsername, nameof(discordUsername));

        Id = id;
        DiscordUsername = discordUsername;
    }

    public void AddAccount(string accountId, string accountName)
    {
        var accountAlreadyExists = _accounts.Any(a => a.Id == accountId);

        if (accountAlreadyExists)
            throw new ArgumentException($"{accountId} has already been added");

        var account = new UserAccount(accountId, accountName);
        _accounts.Add(account);
    }
}
