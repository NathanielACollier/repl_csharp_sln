using System;
using System.Threading.Tasks;

namespace NugetPackageSearch.repos;

public class MainWindowRepo
{
    private nac.Forms.Form myForm;
    private models.MainWindowModel model;
    private repos.NugetPackage nuget;

    private MainWindowRepo()
    {
        
    }

    public static async Task<MainWindowRepo> CreateAndRun()
    {
        var window = new MainWindowRepo();
        await window.run();

        return window;
    }
    
    private async Task run()
    {
        myForm = nac.Forms.Form.NewForm();

        model = new models.MainWindowModel();
        myForm.DataContext = model;

        myForm.Title = $"Nuget Package Search v{model.Version}";

        nuget = await repos.NugetPackage.Create();

        await buildAndDisplayUI();
    }


    private async Task buildAndDisplayUI()
    {
        myForm.HorizontalGroup(hg =>
        {
            hg.Text("Nuget Package: ")
                .TextBoxFor(nameof(model.searchTerm))
                .Button("Search", async () =>
                {
                    await runNugetSearch();
                });
        })
        .Display();
    }

    private async Task runNugetSearch()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.searchTerm))
            {
                return;
            }

            var results = await nuget.GetPackagesAsync(searchTerm: model.searchTerm,
                includePrerelease: false,
                exactMatch: false);
            
            
        }
        catch (Exception ex)
        {
            
        }
    }
}