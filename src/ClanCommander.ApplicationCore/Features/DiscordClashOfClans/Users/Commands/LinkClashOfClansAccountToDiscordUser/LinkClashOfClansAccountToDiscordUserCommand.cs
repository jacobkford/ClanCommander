namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Commands.LinkClashOfClansAccountToDiscordUser;

public class LinkClashOfClansAccountToDiscordUserCommand : IRequest<LinkClashOfClansAccountToDiscordUserDto>
{
    public ulong UserId { get; set; }
    
    public string AccountId { get; set; }

    public LinkClashOfClansAccountToDiscordUserCommand(ulong userId, string accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }

    internal class LinkClashOfClansAccountToDiscordUserCommandHandler : IRequestHandler<LinkClashOfClansAccountToDiscordUserCommand, LinkClashOfClansAccountToDiscordUserDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDiscordUserService _userService;
        private readonly IClashOfClansApiPlayerService _playerApiService;

        public LinkClashOfClansAccountToDiscordUserCommandHandler(IServiceProvider serviceProvider, IClashOfClansApiPlayerService clanApiService, IDiscordUserService userService)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            _playerApiService = clanApiService;
        }

        public async Task<LinkClashOfClansAccountToDiscordUserDto> Handle(LinkClashOfClansAccountToDiscordUserCommand request, CancellationToken cancellationToken)
        {
            var requestUserId = DiscordUserId.FromUInt64(request.UserId);
            var requestAccountId = PlayerId.FromString(request.AccountId);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()
                ?? throw new NullReferenceException();

            var accountData = await _playerApiService.GetPlayerAsync(request.AccountId)
                ?? throw new ArgumentException($"Couldn't find an account with the id of '{request.AccountId}'");

            var user = await dbContext.DiscordUsers.FirstOrDefaultAsync(x => x.UserId == requestUserId, cancellationToken);

            if (user is null)
            {
                var discordUsername = await _userService.GetUsername(request.UserId)
                    ?? throw new ArgumentException($"Couldn't find the discord user");

                user = new DiscordUser(requestUserId, discordUsername);
                await dbContext.AddAsync(user, cancellationToken);
            }

            var accountAlreadyLinkedToUser = await dbContext.ClashOfClansUserAccounts.AnyAsync(
                x => x.UserId == requestUserId && x.AccountId == requestAccountId, cancellationToken);

            if (accountAlreadyLinkedToUser)
            {
                throw new ArgumentException("Account has already been linked to this user");
            }

            var account = user.CreateClashOfClansAccount(requestAccountId, accountData.Name);
            await dbContext.AddAsync(account, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new LinkClashOfClansAccountToDiscordUserDto 
            { 
                AccountId = account.AccountId.Value,
                AccountName = account.Name,
                DiscordUserId = user.UserId.Value,
                DiscordUsername = user.Username,
            };
        }
    }
}
