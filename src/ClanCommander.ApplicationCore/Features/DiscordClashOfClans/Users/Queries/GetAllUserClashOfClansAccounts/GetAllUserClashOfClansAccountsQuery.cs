namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Queries.GetAllUserClashOfClansAccounts;

public class GetAllUserClashOfClansAccountsQuery : IRequest<GetAllUserClashOfClansAccountsDto>
{
    public ulong UserId { get; private set; }

    public GetAllUserClashOfClansAccountsQuery(ulong userId)
    {
        UserId = userId;
    }

    internal class GetAllUserClashOfClansAccountsQueryHandler : IRequestHandler<GetAllUserClashOfClansAccountsQuery, GetAllUserClashOfClansAccountsDto>
    {
        private readonly IDbConnection _db;
        private readonly IClashOfClansApiPlayerService _playerApiService;
        private readonly IDiscordUserService _userService;

        public GetAllUserClashOfClansAccountsQueryHandler(IConfiguration configuration, IClashOfClansApiPlayerService playerApiService, IDiscordUserService userService)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
            _playerApiService = playerApiService;
            _userService = userService;
        }

        public async Task<GetAllUserClashOfClansAccountsDto> Handle(GetAllUserClashOfClansAccountsQuery request, CancellationToken cancellationToken)
        {
            var sqlQuery = $@"SELECT ""{nameof(ClashOfClansAccount)}"".""{nameof(ClashOfClansAccount.AccountId)}"" 
                            FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(ClashOfClansAccount)}"" 
                            WHERE ""{nameof(ClashOfClansAccount)}"".""{nameof(ClashOfClansAccount.UserId)}"" = @UserId;";

            var data = await _db.QueryAsync<ClashOfClansAccount>(sqlQuery, new { @UserId = (decimal)request.UserId });

            var accounts = new List<ClashOfClansAccountDto>();

            foreach (var account in data)
            {
                var accountData = await _playerApiService.GetPlayerAsync(account.AccountId.Value)
                    ?? throw new ArgumentException($"Couldn't find Clash of Clans account with the id '{account.AccountId.Value}'");

                var heroes = new Dictionary<string, int>();

                if (accountData.Heroes.Any())
                    foreach (var heroData in accountData.Heroes)
                        heroes.Add(heroData.Name, heroData.Level);

                accounts.Add(new ClashOfClansAccountDto
                {
                    Id = accountData.Tag,
                    Name = accountData.Name,
                    ClanId = accountData.Clan?.Tag ?? "N/A",
                    ClanName = accountData.Clan?.Name ?? "N/A",
                    ClanRole = accountData.Role switch
                    {
                        ClashOfClans.Models.Role.Leader => ClanMemberRole.Leader.Name,
                        ClashOfClans.Models.Role.CoLeader => ClanMemberRole.CoLeader.Name,
                        ClashOfClans.Models.Role.Admin => ClanMemberRole.Elder.Name,
                        ClashOfClans.Models.Role.Member => ClanMemberRole.Member.Name,
                        _ => "N/A",
                    },
                    TownHallLevel = accountData.TownHallLevel,
                    ExpLevel = accountData.ExpLevel,
                    HeroLevels = heroes,
                });
            }

            return new GetAllUserClashOfClansAccountsDto
            {
                UserId = request.UserId,
                ClashOfClansAccounts = accounts,
            };
        }
    }
}
