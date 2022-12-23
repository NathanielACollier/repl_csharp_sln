using nac.Forms;

namespace GitRepoFinder.repos;

public class EditWorkspacesWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.EditWorkspacesWindowModel model;


    public static async Task run(nac.Forms.Form parentForm)
    {
        await parentForm.DisplayChildForm((_child) =>
        {
            myForm = _child;
            model = new models.EditWorkspacesWindowModel();
            myForm.DataContext = model;

            myForm.Title = $"Edit Workspaces";
            
            buildAndDisplayUI();
        }, useIsolatedModelForThisChildForm: true,
            onDisplay: async (_f) =>
            {
                var existingWorkspaces = await repos.WorkspacesRepo.getAll();
                foreach (var wsPath in existingWorkspaces)
                {
                    model.WorkspacePath.Add(new models.WorkspaceModel
                    {
                        Path = wsPath
                    });
                }
            });
    }

    private static void buildAndDisplayUI()
    {
        myForm.HorizontalGroup(h =>
        {
            h.Button("Add", async () =>
            {
                var newWorkspaceFolderPath = await myForm.ShowOpenFolderDialog();
                if (!string.IsNullOrWhiteSpace(newWorkspaceFolderPath) &&
                    System.IO.Directory.Exists(newWorkspaceFolderPath))
                {
                    await repos.WorkspacesRepo.AddWorkspace(newWorkspaceFolderPath);
                    model.WorkspacePath.Add(new models.WorkspaceModel
                    {
                        Path = newWorkspaceFolderPath
                    });
                }
            });
        }).List<models.WorkspaceModel>(nameof(model.WorkspacePath), itemRow =>
        {
            itemRow.HorizontalGroup(h =>
            {
                var ws = itemRow.DataContext as models.WorkspaceModel;
                h.Button("Delete", async () =>
                {
                    await repos.WorkspacesRepo.RemoveWorkspace(ws.Path);
                    model.WorkspacePath.Remove(ws);
                }).TextFor(nameof(ws.Path));
            });
        });
    }
}