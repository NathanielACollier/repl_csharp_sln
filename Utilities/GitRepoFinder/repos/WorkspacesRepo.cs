namespace GitRepoFinder.repos;

public static class WorkspacesRepo
{
    private static List<string> workspaces = new();


    public static Task<List<string>> getAll()
    {
        var tc = new TaskCompletionSource<List<string>>();

        tc.SetResult(workspaces);

        return tc.Task;
    }

    public static Task AddWorkspace(string workspacePath)
    {
        workspaces.Add(workspacePath);
        return Task.CompletedTask;
    }

    public static Task RemoveWorkspace(string workspacePath)
    {
        workspaces.Remove(workspacePath);
        return Task.CompletedTask;
    }
    
}