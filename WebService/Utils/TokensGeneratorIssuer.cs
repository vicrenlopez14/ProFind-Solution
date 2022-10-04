using Azure.Communication;
using Azure.Communication.Chat;
using WebService.Models.Generated;

namespace WebService.Utils;

using Azure.Core;
using Azure.Communication.Identity;

public class TokensGeneratorIssuer
{
    private const string ENDPOINTURI = "https://profind-communication-service.communication.azure.com/";

    private const string CONNECTIONSTRING =
        "endpoint=https://profind-communication-service.communication.azure.com/;accesskey=7HPdyV4H5lk4mfzQRuo95AOGGeewY10f57yKPgYJ1DZ1S5qfJbJbG4lahqWdM4aQj2mIESg0Z9Wov9AV8OruSg==";

    private static CommunicationIdentityClient client = new CommunicationIdentityClient(CONNECTIONSTRING);

    public static string GenerateCommunicationId()
    {
        try
        {
            var id = client.CreateUser();
            return id.Value.Id;
        }
        catch
        {
            return null;
        }
    }

    public static ChatAccessInfo IssueToken(string communicationId)
    {
        var tokenResponse = client.GetToken(new CommunicationUserIdentifier(communicationId),
            new[] { CommunicationTokenScope.Chat, CommunicationTokenScope.VoIP, });

        return new ChatAccessInfo
        {
            UserId = communicationId,
            Token = tokenResponse.Value.Token,
            ExpiresOn = tokenResponse.Value.ExpiresOn,
            EndPointUri = ENDPOINTURI
        };
    }

    // Create chat client
    public static ChatClient CreateChatClient(string token)
    {
        var credential = new CommunicationTokenCredential(token);
        return new ChatClient(new Uri(ENDPOINTURI), credential);
    }
}