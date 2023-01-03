namespace GitRepoFinder.models;

public class SettingsFileModel
{
    public List<string> workspaces { get; set; } = new();

    public List<FolderCommandModel> commands { get; set; } = new();

}