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
            
            // the json deserialize can cause this to be null so fix that
            if (model.EnvironmentVariables == null)
            {
                model.EnvironmentVariables = new();
            }

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
                        hg.Text("Key: ")
                            .TextBoxFor(nameof(models.FolderCommandEnvironmentVariableModel.Key))
                            .Text("Value: ")
                            .TextBoxFor(nameof(models.FolderCommandEnvironmentVariableModel.Value));
                    });
                });

    }
    
}