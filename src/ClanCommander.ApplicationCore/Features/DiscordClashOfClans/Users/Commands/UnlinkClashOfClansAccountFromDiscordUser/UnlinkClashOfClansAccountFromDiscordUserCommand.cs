namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Commands.UnlinkClashOfClansAccountFromDiscordUser;

public class UnlinkClashOfClansAccountFromDiscordUserCommand : IRequest
{
    public ulong UserId { get; set; }

    public string AccountId { get; set; }

    public UnlinkClashOfClansAccountFromDiscordUserCommand(ulong userId, string accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }

    internal class UnlinkClashOfClansAccountFromDiscordUserCommandHandler : IRequestHandler<UnlinkClashOfClansAccountFromDiscordUserCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public UnlinkClashOfClansAccountFromDiscordUserCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(UnlinkClashOfClansAccountFromDiscordUserCommand request, CancellationToken cancellationToken)
        {
            var requestUserId = DiscordUserId.FromUInt64(request.UserId);
            var requestAccountId = PlayerId.FromString(request.AccountId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var account = await dbContext.ClashOfClansUserAccounts.FirstOrDefaultAsync(
                account => account.AccountId == requestAccountId && account.UserId == requestUserId)
                ?? throw new ArgumentException($"There's no account with the id of {request.AccountId} linked to this user");

            dbContext.Remove(account);
            await dbContext.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
