using nac.Forms;
using nac.Forms.model;

namespace GitRepoFinder.repos;

public static class MainWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.MainWindowModel model;


    public static async Task run()
    {
        myForm = Avalonia.AppBuilder.Configure<nac.Forms.App>()
            .NewForm();

        model = new models.MainWindowModel();
        myForm.DataContext = model;

        myForm.Title = $"Git Repo Finder v{model.Version}";

        await buildAndDisplayUI();
    }

    private static async Task buildAndDisplayUI()
    {
        myForm.HorizontalGroup(h =>
        {
            h.Text("Git Repo Count: ")
                .TextFor(nameof(model.repoCount))
                .Text(" Displayed Count: ")
                .TextFor(nameof(model.displayedGitRepos));
        })
        .HorizontalGroup(h =>
        {
            h.Text("Loading git repos", style: new Style{width = 100})
                .LoadingTextAnimation(style: new Style { width = 30 });
        }, style:new Style{isHiddenModelName = nameof(model.doneGitRepoLoad)})
        .List<models.GitRepoInfo>( nameof(model.displayedGitRepos), itemRow =>
        {
            var repo = itemRow.DataContext as models.GitRepoInfo;

            itemRow.Button("...", async () =>
            {
                repos.os.OpenBrowser(repo.Path);
            }).TextFor(nameof(repo.Name))
            .TextFor(nameof(repo.Path));

        }, style: new Style{isVisibleModelName = nameof(model.doneGitRepoLoad)})
        .Display();
    }
    
    
    
}