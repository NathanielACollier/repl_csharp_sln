namespace GitRepoFinder.repos;

public class settingsFile
{
    public static models.SettingsFileModel read()
    {
        var filePath = getSettingsFilePath();
        if (!System.IO.File.Exists(filePath))
        {
            return new models.SettingsFileModel();
        }

        string json = System.IO.File.ReadAllText(filePath);
        var settings = System.Text.Json.JsonSerializer.Deserialize<models.SettingsFileModel>(json);
        return settings;
    }

    
    public static void write(models.SettingsFileModel settings)
    {
        var filePath = getSettingsFilePath();

        string json = System.Text.Json.JsonSerializer.Serialize(settings);
        System.IO.File.WriteAllText(path: filePath, contents: json);
    }
    
    
    
    private static string getSettingsFilePath()
    {
        var userProfileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var appDirPath =
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(userProfileFolderPath, ".data",
                "GitRepoFinder"));

        return System.IO.Path.Combine(appDirPath.FullName, "settings.json");
    }
}