using nac.Forms.model;

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
            }, onClosing: async (_f) =>
            {
                // save the commands
                await repos.CommandsRepo.saveAll(model.CommandList);

                return false; 
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
        })
            .Text(@"--------------------
Command Placeholders - Can be used in the Args textbox
--------------------
{folderpath} - Path, This is the path to the folder the git repo is in
####################
            ")
            .List<models.FolderCommandModel>(nameof(model.CommandList), itemRow =>
        {
            itemRow.HorizontalGroup(h =>
            {
                var cmd = itemRow.DataContext as models.FolderCommandModel;
                h.Button("Delete", async () =>
                    {
                        await repos.CommandsRepo.Remove(cmd);
                        model.CommandList.Remove(cmd);
                    })
                    .Text("Desc: ")
                    .TextBoxFor(nameof(cmd.Description), style: new Style{width = 50})
                    .Text("Exe: ")
                    .TextBoxFor(nameof(cmd.ExePath), style: new Style { width = 400 })
                    .Text("Args: ")
                    .TextBoxFor(nameof(cmd.Arguments), style: new Style { width = 400 });
            });
        });
    }
}