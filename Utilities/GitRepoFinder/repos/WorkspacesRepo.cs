namespace GitRepoFinder.repos;

public static class WorkspacesRepo
{

    public static Task<List<string>> getAll()
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            return settings.workspaces;
        });
    }

    public static Task AddWorkspace(string workspacePath)
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            settings.workspaces.Add(workspacePath);
            repos.settingsFile.write(settings);
        });
    }

    public static Task RemoveWorkspace(string workspacePath)
    {
        return Task.Run(() => {
            var settings = repos.settingsFile.read();
            settings.workspaces.Remove(workspacePath);
            repos.settingsFile.write(settings);
        });
    }



    
}