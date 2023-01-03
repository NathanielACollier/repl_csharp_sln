using Avalonia.Input;
using GitRepoFinder.models;
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
                    .TextFor(nameof(model.repoDisplayCount))
                    .Button("Edit Workspaces", async () =>
                    {
                        await repos.EditWorkspacesWindowRepo.run(myForm);
                        await RefreshGitRepos();
                    }, new Style
                    {
                        contextMenuItems = new nac.Forms.model.MenuItem[]
                        {
                            new nac.Forms.model.MenuItem
                            {
                                Header = "Edit Commands",
                                Action = async () =>
                                {
                                    await repos.EditCommandsWindowRepo.run(myForm);
                                    await repos.CommandsRepo.RefreshCommandListCache();
                                    await RefreshGitRepos();
                                }
                            }
                        }
                    });
            })
        .HorizontalGroup(h =>
        {
            h.Text("Loading git repos", style: new Style{width = 100})
                .LoadingTextAnimation(style: new Style { width = 30 });
        }, style:new Style{isHiddenModelName = nameof(model.doneGitRepoLoad)})
        .VerticalGroup(v =>
        {
            v
            .HorizontalGroup(h =>
            {
                h.Button("Refresh", async () =>
                {
                    await RefreshGitRepos();
                }).Button("Filter", async () =>
                {
                    FilterRepos();
                }).TextBoxFor(nameof(model.filterText), onKeyPress: (key) =>
                {
                    // filter the repos if they hit enter in the textbox
                    if (key.Key == Key.Return && key.KeyModifiers == KeyModifiers.None)
                    {
                        FilterRepos();
                    }   
                });
            }, style: new Style{ height = 30})
            .List<models.GitRepoInfo>(nameof(model.displayedGitRepos), itemRow =>
            {
                var repo = itemRow.DataContext as models.GitRepoInfo;

                var commandMenuItems = generateMenuItemsForAllFolderComppands(repo);

                itemRow.HorizontalGroup(h =>
                {
                    h.Button("...", async () => { repos.os.OpenBrowser(repo.Path); },
                            style: new Style
                            {
                                width = 30,
                                contextMenuItems = commandMenuItems
                            })
                        .TextFor(nameof(repo.Name), style: new Style{width = 300})
                        .TextFor(nameof(repo.Path));
                });
            });
        }, style: new Style{isVisibleModelName = nameof(model.doneGitRepoLoad)})
        .Display(onDisplay: async (_f) =>
        {
            await repos.CommandsRepo.RefreshCommandListCache();
            await RefreshGitRepos();
        });
    }

    private static nac.Forms.model.MenuItem[] generateMenuItemsForAllFolderComppands(GitRepoInfo repo)
    {
        var items = new List<nac.Forms.model.MenuItem>();
        var cmdList = repos.CommandsRepo.GetAllCached();

        foreach (var c in cmdList)
        {
            var item = new nac.Forms.model.MenuItem();

            string commandArguments = repos.StringFormat.OskarFormat(c.Arguments, new
            {
                filepath = repo.Path
            });

            item.Header = c.Description;
            item.Action = () =>
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(c.ExePath, commandArguments));
            };
            items.Add(item);
        }

        return items.ToArray();
    }

    private static void FilterRepos()
    {
        if (string.IsNullOrWhiteSpace(model.filterText))
        {
            model.filterText = "";
        }
        
        model.displayedGitRepos.Clear();
        foreach (var r in model.gitRepos)
        {
            if (r.Name.IndexOf(model.filterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                model.displayedGitRepos.Add(r);
            }
        }

        model.repoDisplayCount = model.displayedGitRepos.Count;
    }

    private static async Task RefreshGitRepos()
    {
        model.doneGitRepoLoad = false;
        try
        {
            model.gitRepos.Clear();
            model.displayedGitRepos.Clear();

            var workspaces = await repos.WorkspacesRepo.getAll();
            
            var repoList = await repos.GitRepo.refreshGitRepos(workspaces);
            // sort it
            repoList = repoList.OrderBy(i => i.Name)
                .ToList();

            model.repoCount = repoList.Count;
            model.repoDisplayCount = repoList.Count;


            foreach (var r in repoList)
            {
                model.gitRepos.Add(r);
                model.displayedGitRepos.Add(r);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            model.doneGitRepoLoad = true;
        }
    }
    
    
    
    
    
}