using System;
using System.Collections.Generic;

namespace commonUtilitiesLib;

public static class settings
{

    public static string Get(string key){
        string userProfileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        string settingsFilePath = System.IO.Path.Combine(userProfileFolderPath, "settings.json");
        string settingsJsonText = System.IO.File.ReadAllText(settingsFilePath);
        var settingsModel = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,object>>(json: settingsJsonText);

        return Convert.ToString(settingsModel[key]);
    }
}
