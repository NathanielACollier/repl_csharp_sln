namespace GitRepoFinder.repos;

public static class CommandsRepo
{
    
    public static Task<List<models.FolderCommandModel>> getAll()
    {
        var tc = new TaskCompletionSource<List<models.FolderCommandModel>>();

        var settings = repos.settingsFile.read();
        tc.SetResult(settings.commands);

        return tc.Task;
    }

    public static async Task saveAll(IEnumerable<models.FolderCommandModel> commands)
    {
        var settings = repos.settingsFile.read();
        
        settings.commands.Clear();
        settings.commands.AddRange(commands);
        
        repos.settingsFile.write(settings);
    }

    public static Task Add(models.FolderCommandModel command)
    {
        var settings = repos.settingsFile.read();
        settings.commands.Add(command);
        repos.settingsFile.write(settings);
        
        return Task.CompletedTask;
    }

    public static Task Remove(models.FolderCommandModel command)
    {
        var settings = repos.settingsFile.read();
        settings.commands.Remove(command);
        repos.settingsFile.write(settings);
        
        return Task.CompletedTask;
    }
}