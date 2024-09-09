using System.Threading.Tasks;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;

public static class GraphAPIRepo
{


    public static async Task<string> GetUser(string token)
    {
        var http = new nac.http.HttpClient("https://graph.microsoft.com/",
            new nac.http.model.HttpClientConfigurationOptions
            {
                bearerToken = token
            });

        var currentUser = await http.GetJSONAsync<System.Text.Json.Nodes.JsonNode>("Me");

        return "";
    } 
}