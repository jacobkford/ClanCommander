using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace ClanCommander.Infrastructure.Services;

public class MailService
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

