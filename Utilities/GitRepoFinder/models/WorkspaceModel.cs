namespace GitRepoFinder.models;

public class WorkspaceModel : nac.Forms.model.ViewModelBase
{

    public string Path
    {
        get { return GetValue(() => Path); }
        set { SetValue(() => Path, value);}
    }
}