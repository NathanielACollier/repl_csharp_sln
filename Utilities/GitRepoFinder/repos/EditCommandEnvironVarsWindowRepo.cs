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
        
        
    }
    
}