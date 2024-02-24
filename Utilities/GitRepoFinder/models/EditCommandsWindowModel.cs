using System.Collections.ObjectModel;

namespace GitRepoFinder.models;

public class EditCommandsWindowModel : nac.ViewModelBase.ViewModelBase
{
    public ObservableCollection<models.FolderCommandModel> CommandList
    {
        get { return GetValue(() => CommandList); }
    }
}