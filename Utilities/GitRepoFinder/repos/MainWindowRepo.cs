﻿using Avalonia.Input;
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
                    .TextFor(nameof(model.displayedGitRepos))
                    .Button("Edit Workspaces", async () =>
                    {
                        await repos.EditWorkspacesWindowRepo.run(myForm);
                        await RefreshGitRepos();
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

                itemRow.HorizontalGroup(h =>
                {
                    h.Button("...", async () => { repos.os.OpenBrowser(repo.Path); })
                        .TextFor(nameof(repo.Name))
                        .TextFor(nameof(repo.Path));
                });
            });
        }, style: new Style{isVisibleModelName = nameof(model.doneGitRepoLoad)})
        .Display(onDisplay: async (_f) =>
        {
            await RefreshGitRepos();
        });
    }

    private static void FilterRepos()
    {
        if (string.IsNullOrWhiteSpace(model.filterText))
        {
            return; // no need to filter on empty and it would be a problem if it was null
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