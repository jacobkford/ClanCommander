namespace ClanCommander.ApplicationCore.Entities;

internal class User : Entity, IAggregateRoot
{
    public ulong Id { get; private set; }

    public string DiscordUsername { get; private set; }

    private readonly List<UserAccount> _accounts = new List<UserAccount>();
    public IReadOnlyCollection<UserAccount> Accounts => _accounts.AsReadOnly();

    private User() { }

    public User(ulong id, string discordUsername)
    {
        Id = Guard.Against.InvalidDiscordSnowflakeId(id, nameof(id));
        DiscordUsername = Guard.Against.NullOrWhiteSpace(discordUsername, nameof(discordUsername));
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
