using ClanCommander.ApplicationCore.Features.Discord.Guilds.ClientEvents;
using Dapper;
using Npgsql;
using System.Linq;

namespace ClanCommander.IntegrationTests.DiscordSocketClientEvents;

public class LeftDiscordGuildClientEventTests : TestBase
{
    // Test Guild One is to test fully setup guilds
    private readonly DiscordGuildId _testGuildOneId = DiscordGuildId.FromUInt64(760910445686161488u);
    private readonly string _testGuildOneName = "Test1";
    private readonly DiscordUserId _testGuildOneOwner = DiscordUserId.FromUInt64(339924145909399562u);

    public LeftDiscordGuildClientEventTests() : base()
    {
        this.SeedTestData();
    }

    [Fact]
    public async void ShouldDeleteRegisteredDiscordGuild_WhenBotHasLeftServer()
    {
        await Mediator.Publish(new LeftDiscordGuildClientEvent(_testGuildOneId.Value));

        var db = new NpgsqlConnection(Configuration.GetConnectionString("PostgreSQL"));

        var result = (await db.QueryAsync<object>(
            $@"SELECT * FROM ""{ApplicationDbContext.DISCORD_SCHEMA}"".""{nameof(RegisteredDiscordGuild)}"" 
            WHERE ""{nameof(RegisteredDiscordGuild)}"".""{nameof(RegisteredDiscordGuild.GuildId)}"" = @GuildId;",
            new
            {
                @GuildId = (decimal)_testGuildOneId.Value
            })).SingleOrDefault();

        result.Should().BeNull();
    }

    private void SeedTestData()
    {
        ApplicationDbContext.DiscordGuilds.Add(new RegisteredDiscordGuild(_testGuildOneId, _testGuildOneName, _testGuildOneOwner));
        ApplicationDbContext.SaveChanges();
    }
}
