using System.Text.Json;
using System.Threading.Tasks;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;

public static class GraphAPIRepo
{


    public static async Task<string> GetUser(string token)
    {
        var http = new nac.http.HttpClient("https://graph.microsoft.com/",
            new nac.http.model.HttpClientConfigurationOptions
            {
                useWindowsAuth = false,
                useBearerTokenAuthentication = true,
                bearerToken = token
            });

        var currentUser = await http.GetJSONAsync<System.Text.Json.Nodes.JsonNode>("v1.0/me");

        string upn = currentUser["userPrincipalName"].Deserialize<string>();

        return upn;
    } 
}