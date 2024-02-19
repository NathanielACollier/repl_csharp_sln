using System.Collections.ObjectModel;

namespace GitRepoFinder.models;

public class EditWorkspacesWindowModel : nac.ViewModelBase.ViewModelBase
{
    public ObservableCollection<models.WorkspaceModel> WorkspacePath
    {
        get { return GetValue(() => WorkspacePath); }
    }
}