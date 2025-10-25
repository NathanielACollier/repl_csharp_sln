namespace GitRepoFinder.repos;

public static class CommandsRepo
{
    private static nac.Logging.Logger log = new();
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
    
    
    
    
    public static void RunCommand(models.GitRepoInfo repo, models.FolderCommandModel command)
    {
        string commandArguments = repos.StringFormat.OskarFormat(command.Arguments, new
        {
            folderpath = repo.Path
        });
            
        log.Info($"Running command: EXE[{command.ExePath}] Arguments[{commandArguments}]");

        var procStartInfo = new System.Diagnostics.ProcessStartInfo(command.ExePath, commandArguments);

        var evList = command.EnvironmentVariables?.Where(ev => !string.IsNullOrWhiteSpace(ev.Key)) ?? [];

        foreach (var ev in evList)
        {
            string evValue = ev.Value;
            string originalValue = procStartInfo.EnvironmentVariables[ev.Key];
            if (!string.IsNullOrWhiteSpace(originalValue) &&
                string.Equals(ev.Key, "path", StringComparison.OrdinalIgnoreCase))
            {
                evValue = ev.Value + ":" + originalValue;
            }
            procStartInfo.EnvironmentVariables[ev.Key] = evValue;
            log.Info($"Set Environment Variable: {ev.Key}={evValue}");
        }
            
        System.Diagnostics.Process.Start(procStartInfo);
    }
    
    
    
}