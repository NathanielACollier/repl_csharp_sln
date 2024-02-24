namespace GitRepoFinder.models;

public class GitRepoInfo : nac.ViewModelBase.ViewModelBase
{
    
    public string Name
    {
        get { return GetValue(() => Name); }
        set { SetValue(() => Name, value);}
    }

    public string Path
    {
        get { return GetValue(() => Path); }
        set { SetValue(() => Path, value);}
    }
}