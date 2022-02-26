namespace ClanCommander.ApplicationCore.Data;

internal class ApplicationDbContextSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextSeeder> _logger;

    public ApplicationDbContextSeeder(ApplicationDbContext context, ILogger<ApplicationDbContextSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            if (!_context.DiscordGuilds.Any())
            {
                var guilds = new List<RegisteredDiscordGuild>()
                {
                    new RegisteredDiscordGuild(
                        DiscordGuildId.FromUInt64(760910445686161488),
                        "PlaneClashers Bot Test Server",
                        DiscordUserId.FromUInt64(339924145909399562))
                };

                await _context.DiscordGuilds.AddRangeAsync(guilds);
                _logger.LogInformation("Seeded Guilds");
            }

            if (!_context.DiscordUsers.Any())
            {
                var users = new List<DiscordUser>()
                {
                    new DiscordUser(DiscordUserId.FromUInt64(339924145909399562), "Jaycub#2554")
                };

                await _context.DiscordUsers.AddRangeAsync(users);
                _logger.LogInformation("Seeded Users");
            }

            if (!_context.MessageCommandsConfigurations.Any())
            {
                var commandConfigs = new List<GuildMessageCommands>()
                {
                    new GuildMessageCommands(DiscordGuildId.FromUInt64(760910445686161488))
                };

                await _context.MessageCommandsConfigurations.AddRangeAsync(commandConfigs);
                _logger.LogInformation("Seeded Guild Message Command Configurations");
            }

            if (!_context.ClashOfClansUserAccounts.Any())
            {
                var accounts = new List<ClashOfClansAccount>()
                {
                    new ClashOfClansAccount(PlayerId.FromString("#PQU9QLP2V"), DiscordUserId.FromUInt64(339924145909399562), "JAY")
                };

                await _context.ClashOfClansUserAccounts.AddRangeAsync(accounts);
                _logger.LogInformation("Seeded User Accounts");
            }

            if (!_context.GuildClans.Any())
            {
                var clan = new GuildClan(ClanId.FromString("#9UGQ0GL"), "PlaneClashers", DiscordGuildId.FromUInt64(760910445686161488));
                clan.AddClanMember(PlayerId.FromString("#PQU9QLP2V"), DiscordUserId.FromUInt64(339924145909399562), ClanMemberRole.Leader);

                await _context.GuildClans.AddAsync(clan);
                _logger.LogInformation("Seeded Clans and Members");
            }

            if (!_context.GuildClanMemberRoles.Any())
            {
                var roles = new List<GuildClanMemberRole>()
                {
                    new GuildClanMemberRole(DiscordGuildId.FromUInt64(760910445686161488), 761628609248624690, ClanMemberRole.Leader),
                    new GuildClanMemberRole(DiscordGuildId.FromUInt64(760910445686161488), 761628584921006112, ClanMemberRole.CoLeader),
                    new GuildClanMemberRole(DiscordGuildId.FromUInt64(760910445686161488), 761628566001287221, ClanMemberRole.Elder),
                    new GuildClanMemberRole(DiscordGuildId.FromUInt64(760910445686161488), 761628550280904745, ClanMemberRole.Member),
                };

                await _context.GuildClanMemberRoles.AddRangeAsync(roles);
                _logger.LogInformation("Seeded Guild Clan Roles");
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
