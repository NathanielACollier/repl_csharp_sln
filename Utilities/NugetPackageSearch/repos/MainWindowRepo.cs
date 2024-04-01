using System;
using System.Threading.Tasks;
using Avalonia.Input;

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
        
        
        
        buildAndDisplayUI();
    }


    private void buildAndDisplayUI()
    {
        myForm.HorizontalGroup(hg =>
        {
            hg.Text("Nuget Package: ")
                .TextBoxFor(nameof(model.searchTerm), onKeyPress: async keyArg =>
                {
                    if (keyArg.Key == Key.Enter)
                    {
                        await runNugetSearch();
                    }
                })
                .Button("Search", async () =>
                {
                    await runNugetSearch();
                });
        })
        .Table<models.Nuget.NugetPackageInfo>(itemsModelFieldName: nameof(model.Packages))
        .Display(onDisplay: async (__f) =>
        {
            nuget = await repos.NugetPackage.Create();
        });
    }

    private async Task runNugetSearch()
    {
        try
        {
            model.Packages.Clear();
            if (string.IsNullOrWhiteSpace(model.searchTerm))
            {
                return;
            }

            var results = await nuget.GetPackagesAsync(searchTerm: model.searchTerm,
                includePrerelease: false,
                exactMatch: false);

            foreach (var r in results)
            {
                var pkgInfo = new models.Nuget.NugetPackageInfo(r);
                model.Packages.Add(pkgInfo);
            }
        }
        catch (Exception ex)
        {
            repos.ErrorWindowRepo.run(parentForm: myForm, exception: ex);
        }
    }
}