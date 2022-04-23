namespace ClanCommander.ApplicationCore.Features.DiscordClashOfClans.Clans.Queries.GetClanRoster;

public class GetClanMembersQuery : IRequest<GetClanMembersDto>
{
    public string ClanId { get; private set; }
    public ulong GuildId { get; private set; }

    public GetClanMembersQuery(string clanId, ulong guildId)
    {
        ClanId = clanId.ToUpper();
        GuildId = guildId;
    }

    internal class GetClanRosterQueryHandler : IRequestHandler<GetClanMembersQuery, GetClanMembersDto?>
    {
        private readonly IDbConnection _db;
        private readonly IClashOfClansApiClanService _clanApiService;

        public GetClanRosterQueryHandler(IConfiguration configuration, IClashOfClansApiClanService clanApiService)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
            _clanApiService = clanApiService;
        }

        // TODO: SQL query & object mapping needs refactoring
        public async Task<GetClanMembersDto?> Handle(GetClanMembersQuery request, CancellationToken cancellationToken)
        {
            var clanData = await _clanApiService.GetClanAsync(request.ClanId);
            if (clanData is null)
            {
                throw new ArgumentException($"Clan with the Id of '{request.ClanId}' was not found.");
            }

            var data = new GetClanMembersDto
            {
                ClanId = clanData!.Tag,
                ClanName = clanData.Name,
                DiscordGuildId = default,
                ClanBadgeUrl = clanData?.BadgeUrls.Large?.ToString() ?? "",
            }; ;

            var clanRosterData = clanData?.MemberList;
            var roster = new List<ClanMemberDto>();

            var sqlQuery = $@"SELECT ""{nameof(GuildClan)}"".*, ""{nameof(GuildClanMember)}"".* 
                            FROM ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClan)}"" 
                            INNER JOIN ""{ApplicationDbContext.DISCORDCLASHOFCLANS_SCHEMA}"".""{nameof(GuildClanMember)}"" 
                                ON ""{nameof(GuildClan)}"".""{nameof(GuildClan.Id)}"" = ""{nameof(GuildClanMember)}"".""ClanId"" 
                            WHERE ""{nameof(GuildClan)}"".""{nameof(GuildClan.ClanId)}"" = @ClanId
                                AND ""{nameof(GuildClan)}"".""{nameof(GuildClan.GuildId)}"" = @GuildId;";

            await _db.QueryAsync<GuildClan, GuildClanMember, GuildClan>(sqlQuery, param: new { @ClanId = request.ClanId, @GuildId = (decimal)request.GuildId }, 
                splitOn: $"{nameof(GuildClanMember.MemberId)}", map: (clan, member) =>
                {
                    if (data.DiscordGuildId is 0UL)
                        data.DiscordGuildId = clan.GuildId.Value;

                    var accountData = clanRosterData?.SingleOrDefault(clanMember => clanMember.Tag == member.MemberId.Value);
                    if (accountData is not null)
                    {
                        roster.Add(new ClanMemberDto
                        {
                            Id = accountData.Tag,
                            Name = accountData.Name,
                            ExpLevel = accountData.ExpLevel,
                            UserId = member.UserId.Value,
                            ClanRole = accountData.Role switch
                            {
                                ClashOfClans.Models.Role.Leader => ClanMemberRole.Leader.Name,
                                ClashOfClans.Models.Role.CoLeader => ClanMemberRole.CoLeader.Name,
                                ClashOfClans.Models.Role.Admin => ClanMemberRole.Elder.Name,
                                ClashOfClans.Models.Role.Member => ClanMemberRole.Member.Name,
                                _ => "Not Found",
                            },
                        });

                        clanRosterData?.Remove(accountData);
                    }
                    return clan;
                });

            // Checking if there's any clan members that haven't been registered in Discord
            if (clanRosterData is not null && clanRosterData.Any())
            {
                foreach (var member in clanRosterData)
                {
                    roster.Add(new ClanMemberDto
                    {
                        Id = member.Tag,
                        Name = member.Name,
                        ExpLevel = member.ExpLevel,
                        UserId = default,
                        ClanRole = member.Role switch
                        {
                            ClashOfClans.Models.Role.Leader => ClanMemberRole.Leader.Name,
                            ClashOfClans.Models.Role.CoLeader => ClanMemberRole.CoLeader.Name,
                            ClashOfClans.Models.Role.Admin => ClanMemberRole.Elder.Name,
                            ClashOfClans.Models.Role.Member => ClanMemberRole.Member.Name,
                            _ => "Not Found",
                        },
                    });
                }
            }
            data.Members = roster;
            return data;
        }
    }
}
