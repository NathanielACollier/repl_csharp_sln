namespace NugetPackageSearch.repos;

public static class MainWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.MainWindowModel model;
    
    public static async Task run()
    {
        myForm = nac.Forms.Form.NewForm();

        model = new models.MainWindowModel();
        myForm.DataContext = model;

        myForm.Title = $"Git Repo Finder v{model.Version}";

        await buildAndDisplayUI();
    }
    
    
    
}