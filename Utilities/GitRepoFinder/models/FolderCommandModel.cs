namespace GitRepoFinder.models;

public class FolderCommandModel : nac.Forms.model.ViewModelBase
{
    public string CommandText
    {
        get { return GetValue(() => CommandText); }
        set { SetValue(() => CommandText, value);}
    }
}