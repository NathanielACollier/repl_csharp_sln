using System.Collections.ObjectModel;

namespace GitRepoFinder.models;

public class EditCommandsWindowModel : nac.Forms.model.ViewModelBase
{
    public ObservableCollection<models.FolderCommandModel> CommandList
    {
        get { return GetValue(() => CommandList); }
    }
}