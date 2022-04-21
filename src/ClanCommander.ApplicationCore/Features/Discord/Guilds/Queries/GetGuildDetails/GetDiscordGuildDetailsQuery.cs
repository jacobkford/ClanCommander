namespace ClanCommander.ApplicationCore.Features.Discord.Guilds.Queries.GetGuildDetails;

public class GetDiscordGuildDetailsQuery : IRequest<DiscordGuildDetailsDto>
{
    public ulong GuildId { get; set; }

    public GetDiscordGuildDetailsQuery(ulong guildId)
    {
        GuildId = guildId;
    }

    internal class GetDiscordGuildDetailsQueryHandler : IRequestHandler<GetDiscordGuildDetailsQuery, DiscordGuildDetailsDto?>
    {
        private readonly IDbConnection _db;

        public GetDiscordGuildDetailsQueryHandler(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));
        }

        public async Task<DiscordGuildDetailsDto?> Handle(GetDiscordGuildDetailsQuery request, CancellationToken cancellationToken)
        {
            var sqlQuery = $@"SELECT 
                                ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" AS ""{nameof(DiscordGuildDetailsDto.Id)}"",
                                ""{nameof(RegisteredDiscordGuild.Name)}"" AS ""{nameof(DiscordGuildDetailsDto.Name)}"",
                                ""{nameof(RegisteredDiscordGuild.OwnerId)}"" AS ""{nameof(DiscordGuildDetailsDto.OwnerId)}"",
                                ""{nameof(GuildMessageCommands.MessageCommandPrefix)}"" AS ""{nameof(DiscordGuildDetailsDto.MessageCommandPrefix)}"" 
                            FROM ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(RegisteredDiscordGuild)}"" 
                            LEFT JOIN ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(GuildMessageCommands)}"" 
                                ON ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" = ""{nameof(GuildMessageCommands)}"".""{nameof(GuildMessageCommands.GuildId)}"" 
                            WHERE ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" = @GuildId;";

            return await _db.QuerySingleOrDefaultAsync<DiscordGuildDetailsDto>(sqlQuery, new { @GuildId = (decimal)request.GuildId });
        }
    }
}
