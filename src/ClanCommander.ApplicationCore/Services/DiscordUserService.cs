namespace ClanCommander.ApplicationCore.Services;

internal class DiscordUserService : IDiscordUserService
{
    private readonly HttpClient _httpClient;
    private readonly ulong _botOwnerId;

    public DiscordUserService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _botOwnerId = ulong.Parse(configuration["Discord:BotOwnerId"]);
        _httpClient = httpClientFactory.CreateClient("DiscordAPI");
    }

    public async Task<string?> GetUsername(ulong userId)
    {
        var dataString = await _httpClient.GetFromJsonAsync<JsonDocument>($"users/{userId}");
        var name = dataString?.RootElement.GetProperty("username").GetString();
        var discriminator = dataString?.RootElement.GetProperty("discriminator").GetString();

        if (name is null || discriminator is null)
            return null;

        return $"{name}#{discriminator}";

    }

    public bool IsBotOwner(ulong userId)
    {
        return _botOwnerId == userId;
    }

    public async Task<bool> IsGuildOwner(ulong guildId, ulong userId)
    {
        var dataString = await _httpClient.GetFromJsonAsync<JsonDocument>($"guilds/{guildId}");
        var owner = dataString?.RootElement.GetProperty("owner_id").GetString();
        return owner == userId.ToString();
    }
}
