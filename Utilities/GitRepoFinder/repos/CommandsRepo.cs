namespace GitRepoFinder.repos;

public static class CommandsRepo
{

    private static List<models.FolderCommandModel> commandListCache;
    
    public static Task<List<models.FolderCommandModel>> getAll()
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            return settings.commands;
        });
    }

    public static Task saveAll(IEnumerable<models.FolderCommandModel> commands)
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            
            settings.commands.Clear();
            settings.commands.AddRange(commands);
            
            repos.settingsFile.write(settings);
        });
    }

    public static Task Add(models.FolderCommandModel command)
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            settings.commands.Add(command);
            repos.settingsFile.write(settings);
        
        });
    }

    public static Task Remove(models.FolderCommandModel command)
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            settings.commands.Remove(command);
            repos.settingsFile.write(settings);
        
        });
    }

    public static async Task RefreshCommandListCache()
    {
        commandListCache = null;
        commandListCache = await getAll();
    }

    public static List<models.FolderCommandModel> GetAllCached()
    {
        return commandListCache;
    }
}