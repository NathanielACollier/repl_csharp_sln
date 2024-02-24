using System.Collections.ObjectModel;

namespace GitRepoFinder.models;

public class MainWindowModel : nac.ViewModelBase.ViewModelBase
{


    public ObservableCollection<models.GitRepoInfo> gitRepos
    {
        get { return GetValue(() => gitRepos); }
    }
    
    public ObservableCollection<models.GitRepoInfo> displayedGitRepos
    {
        get { return GetValue(() => displayedGitRepos); }
    }

    public string filterText
    {
        get { return GetValue(() => filterText); }
        set { SetValue(() => filterText, value);}
    }


    public bool doneGitRepoLoad
    {
        get { return GetValue(() => doneGitRepoLoad); }
        set { SetValue(() => doneGitRepoLoad, value);}
    }


    public int repoCount
    {
        get { return GetValue(() => repoCount); }
        set { SetValue(() => repoCount, value);}
    }


    public int repoDisplayCount
    {
        get { return GetValue(() => repoDisplayCount); }
        set { SetValue(() => repoDisplayCount, value);}
    }
    

    public string Version
    {
        get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); }
    }
    
    
    
}