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

        myForm.Title = $"Nuget Package Search v{model.Version}";

        await buildAndDisplayUI();
    }


    private static async Task buildAndDisplayUI()
    {
        myForm.HorizontalGroup(hg =>
        {
            hg.Text("Nuget Package: ")
                .TextBoxFor(nameof(model.searchTerm))
                .Button("Search", async () =>
                {

                });
        })
        .Display();
    }
}