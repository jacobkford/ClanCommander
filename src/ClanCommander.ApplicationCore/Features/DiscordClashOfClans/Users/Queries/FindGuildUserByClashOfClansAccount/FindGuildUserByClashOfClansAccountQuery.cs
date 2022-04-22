namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Users.Queries.FindGuildUserByClashOfClansAccount;

public class FindGuildUserByClashOfClansAccountQuery : IRequest<FindGuildUserByClashOfClansAccountDto>
{
    public ulong GuildId { get; private set; }
    public string AccountId { get; private set; }

    public FindGuildUserByClashOfClansAccountQuery(ulong guildId, string accountId)
    {
        GuildId = guildId;
        AccountId = accountId.ToUpper();
    }

    internal class FindGuildUserByClashOfClansAccountQueryHandler : IRequestHandler<FindGuildUserByClashOfClansAccountQuery, FindGuildUserByClashOfClansAccountDto>
    {
        private readonly IDbConnection _db;
        private readonly IClashOfClansApiPlayerService _playerApiService;
        private readonly IDiscordUserService _userService;

        public FindGuildUserByClashOfClansAccountQueryHandler(IConfiguration configuration, IClashOfClansApiPlayerService playerApiService, IDiscordUserService userService)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
            _playerApiService = playerApiService;
            _userService = userService;
        }

        public async Task<FindGuildUserByClashOfClansAccountDto> Handle(FindGuildUserByClashOfClansAccountQuery request, CancellationToken cancellationToken)
        {
            var sqlQuery = $@"SELECT ""{nameof(GuildClanMember)}"".* 
                            FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClan)}""
                            INNER JOIN ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                                ON ""{nameof(GuildClanMember)}"".""{nameof(GuildClanMember.MemberId)}"" = @MemberId
                            WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.GuildId)}"" = @GuildId;";

            var clanMember = await _db.QuerySingleOrDefaultAsync<GuildClanMember>(
                sqlQuery, new { @MemberId = request.AccountId, @GuildId = (decimal)request.GuildId });

            if (clanMember is null)
                throw new ArgumentException($"Couldn't find a user linked with an account with the id '{request.AccountId}' in this server");

            var accountData = await _playerApiService.GetPlayerAsync(request.AccountId)
                ?? throw new ArgumentException($"Couldn't find Clash of Clans account with the id '{request.AccountId}'");

            var discordUsername = await _userService.GetUsername(clanMember.UserId.Value);
            var heroes = new Dictionary<string, int>();

            if (accountData.Heroes.Any())
                foreach (var heroData in accountData.Heroes)
                    heroes.Add(heroData.Name, heroData.Level);

            return new FindGuildUserByClashOfClansAccountDto
            {
                UserId = clanMember.UserId.Value,
                Username = discordUsername ?? "",
                GuildId = request.GuildId,
                ClashOfClansAccount = new ClashOfClansAccountDto
                {
                    Id = request.AccountId,
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
                }
            };
        }
    }
}
