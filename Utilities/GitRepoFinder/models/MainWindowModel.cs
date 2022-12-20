namespace GitRepoFinder.models;

public class MainWindowModel
{
    
    
    public string Version
    {
        get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); }
    }
    
    
    
}