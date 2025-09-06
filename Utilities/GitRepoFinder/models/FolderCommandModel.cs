using System.Collections.ObjectModel;

namespace GitRepoFinder.models;

public class FolderCommandModel : nac.ViewModelBase.ViewModelBase
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


    public ObservableCollection<models.FolderCommandEnvironmentVariableModel> EnvironmentVariables
    {
        get { return GetValue(() => EnvironmentVariables); }
    }
}