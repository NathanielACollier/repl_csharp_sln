namespace GitRepoFinder.models;

public class FolderCommandModel : nac.Forms.model.ViewModelBase
{

    public string Description
    {
        get { return GetValue(() => Description); }
        set { SetValue(() => Description, value);}
    }
    
    public string ExePath
    {
        get { return GetValue(() => ExePath); }
        set { SetValue(() => ExePath, value);}
    }
    
    public string Arguments
    {
        get { return GetValue(() => Arguments); }
        set { SetValue(() => Arguments, value);}
    }
}