namespace GitRepoFinder.repos;

public class EditCommandsWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.EditCommandsWindowModel model;


    public static async Task run(nac.Forms.Form parentForm)
    {
        await parentForm.DisplayChildForm((_child) =>
            {
                myForm = _child;
                model = new models.EditCommandsWindowModel();
                myForm.DataContext = model;

                myForm.Title = $"Edit Commands";
            
                buildAndDisplayUI();
            }, useIsolatedModelForThisChildForm: true,
            onDisplay: async (_f) =>
            {
                var existingCommands = await repos.CommandsRepo.getAll();
                
                foreach (var cmd in existingCommands)
                {
                    model.CommandList.Add(cmd);
                }
            });
    }
    
    
    
    private static void buildAndDisplayUI()
    {
        myForm.HorizontalGroup(h =>
        {
            h.Button("Add", async () =>
            {
                model.CommandList.Add(new());
            });
        }).List<models.FolderCommandModel>(nameof(model.CommandList), itemRow =>
        {
            itemRow.HorizontalGroup(h =>
            {
                var cmd = itemRow.DataContext as models.FolderCommandModel;
                h.Button("Delete", async () =>
                    {
                        await repos.CommandsRepo.Remove(cmd);
                        model.CommandList.Remove(cmd);
                    })
                    .Text("Command Text: ")
                    .TextBoxFor(nameof(cmd.CommandText));
            });
        });
    }
}