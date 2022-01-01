namespace ClanCommander.ApplicationCore.Services;

internal class MailService : IMailService
{
    private GmailService GmailService { get; set; }

    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/gmail-dotnet-quickstart.json
    private static readonly string[] Scopes = { GmailService.Scope.GmailModify };
    private static readonly string ApplicationName = "PlaneClashers Discord Bot";

    public MailService()
    {
        string credentialPath = "token.json";

        using var stream = new FileStream("appsettings.json", FileMode.Open, FileAccess.Read);

        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.FromStream(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credentialPath, true)).Result;

        GmailService = new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }
}

