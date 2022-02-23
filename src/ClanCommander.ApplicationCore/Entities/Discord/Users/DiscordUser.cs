﻿namespace ClanCommander.ApplicationCore.Entities.Discord.Users;

internal class DiscordUser : Entity, IAggregateRoot
{
    public DiscordUserId UserId { get; private set; }

    public string Username { get; private set; }

    public IReadOnlyCollection<UserClashOfClansAccount> Accounts => _accounts.AsReadOnly();
    private readonly List<UserClashOfClansAccount> _accounts = new List<UserClashOfClansAccount>();

    private DiscordUser() { }

    public DiscordUser(DiscordUserId id, string discordUsername)
    {
        Guard.Against.InvalidDiscordSnowflakeId(id.Value, nameof(id));
        Guard.Against.NullOrWhiteSpace(discordUsername, nameof(discordUsername));

        UserId = id;
        Username = discordUsername;

        AddDomainEvent(new UserCreatedEvent());
    }

    public void AddAccount(ClashOfClansPlayerId accountId, string accountName)
    {
        var accountAlreadyExists = _accounts.Any(account => account.AccountId == accountId);

        if (accountAlreadyExists)
        {
            throw new ArgumentException($"{accountId} has already been added");
        }

        var account = new UserClashOfClansAccount(accountId, accountName);

        _accounts.Add(account);

        AddDomainEvent(new UserAccountAddedEvent());
    }

    public void RemoveAccount(ClashOfClansPlayerId accountId)
    {
        var account = _accounts.SingleOrDefault(account => account.AccountId == accountId);

        if (account is null)
        {
            throw new ArgumentException($"Couldn't find an account with the id {accountId}");
        }    

        _accounts.Remove(account);

        AddDomainEvent(new UserAccountRemovedEvent());
    }
}
