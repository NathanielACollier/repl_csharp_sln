using System.Collections.ObjectModel;

namespace NugetPackageSearch.models;

public class MainWindowModel : nac.ViewModelBase.ViewModelBase
{

    public string searchTerm
    {
        get { return GetValue(() => searchTerm); }
        set { SetValue(() => searchTerm, value);}
    }


    public ObservableCollection<models.Nuget.NugetPackageInfo> Packages
    {
        get { return GetValue(() => Packages); }
    }
    
    
    
    public string Version
    {
        get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); }
    }
    
    
}