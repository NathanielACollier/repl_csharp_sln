namespace GitRepoFinder.models;

public class FolderCommandEnvironmentVariableModel : nac.ViewModelBase.ViewModelBase
{
    public string Key
    {
        get { return GetValue(() => Key); }
        set { SetValue(() => Key, value); }
    }

    public string Value
    {
        get { return GetValue(() => Value); }
        set { SetValue(() => Value, value); }
    }
    
    
    
}