namespace GitRepoFinder.models;

public class WorkspaceModel : nac.ViewModelBase.ViewModelBase
{

    public string Path
    {
        get { return GetValue(() => Path); }
        set { SetValue(() => Path, value);}
    }
}