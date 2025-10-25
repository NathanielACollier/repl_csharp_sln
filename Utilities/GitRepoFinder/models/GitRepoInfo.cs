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


    public int UncommitedCount
    {
        get { return GetValue(() => UncommitedCount); }
        set { SetValue(() => UncommitedCount, value);}
    }


    public string branchName
    {
        get { return GetValue(() => branchName); }
        set { SetValue(() => branchName, value);}
    }


    public string remoteStatus
    {
        get { return GetValue(() => remoteStatus);  }
        set { SetValue( () => remoteStatus, value);}
    }
    
    
    
}