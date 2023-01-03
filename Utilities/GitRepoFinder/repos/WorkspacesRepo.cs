namespace GitRepoFinder.repos;

public static class WorkspacesRepo
{

    public static Task<List<string>> getAll()
    {
        var tc = new TaskCompletionSource<List<string>>();

        var settings = repos.settingsFile.read();
        tc.SetResult(settings.workspaces);

        return tc.Task;
    }

    public static Task AddWorkspace(string workspacePath)
    {
        var settings = repos.settingsFile.read();
        settings.workspaces.Add(workspacePath);
        repos.settingsFile.write(settings);
        
        return Task.CompletedTask;
    }

    public static Task RemoveWorkspace(string workspacePath)
    {
        var settings = repos.settingsFile.read();
        settings.workspaces.Remove(workspacePath);
        repos.settingsFile.write(settings);
        
        return Task.CompletedTask;
    }



    
}