using GitRepoFinder.models;

namespace GitRepoFinder.repos;

public static class EditCommandEnvironVarsWindowRepo
{
    
    private static nac.Forms.Form myForm;
    private static models.FolderCommandModel model;
    
    
    public static async Task run(nac.Forms.Form parentForm,
                                FolderCommandModel cmd)
    {
        await parentForm.DisplayChildForm((_child) =>
        {
            myForm = _child;
            model = cmd;
            myForm.DataContext = model;

            myForm.Title = $"Edit Environment Variables";
        
            buildAndDisplayUI();
        }, useIsolatedModelForThisChildForm: true,
        onDisplay: async (_f) =>
        {
            
        });
        
    }


    private static void buildAndDisplayUI()
    {
        myForm.HorizontalGroup(hg =>
            {
                hg.Text("Environment Variables")
                    .Button("Add", async () =>
                    {
                        model.EnvironmentVariables.Add(new models.FolderCommandEnvironmentVariableModel());
                    });
            })
            .List<models.FolderCommandEnvironmentVariableModel>(itemSourcePropertyName: nameof(model.EnvironmentVariables), 
                populateItemRow: environVarRow =>
                {
                    environVarRow.HorizontalGroup(hg =>
                    {
                        hg.Button("Delete", onClick: async () =>
                            {
                                var envVarEntry = hg.DataContext as models.FolderCommandEnvironmentVariableModel;
                                model.EnvironmentVariables.Remove(envVarEntry);
                            })
                            .Text("Key: ")
                            .TextBoxFor(nameof(models.FolderCommandEnvironmentVariableModel.Key))
                            .Text("Value: ")
                            .TextBoxFor(nameof(models.FolderCommandEnvironmentVariableModel.Value));
                    });
                });

    }
    
}