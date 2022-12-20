namespace GitRepoFinder.repos;

public static class GitRepo
{
    public static async Task<List<models.GitRepoInfo>> refreshGitRepos(IEnumerable<string> rootWorkspacePaths)
    {
        return await Task.Run(() =>
        {
            var repoList = new List<models.GitRepoInfo>();
            
            try
            {
                foreach (var workspacePath in rootWorkspacePaths)
                {
                    var repoPaths = getRepoPaths(workspacePath);
                    foreach (var path in repoPaths)
                    {
                        var dirInfo = new System.IO.DirectoryInfo(path);
                        var repo = new models.GitRepoInfo
                        {
                            Path = dirInfo.FullName,
                            Name = dirInfo.Name
                        };

                        repoList.Add(repo);
                    }
                }
            }
            catch (Exception ex)
            {
                repos.log.Error($"Error refreshing git repos: {ex}");
            }

            return repoList;
        });
    }

    private static IEnumerable<string> getRepoPaths(string workspacePath)
    {
        foreach (var folderPath in System.IO.Directory.EnumerateDirectories(workspacePath))
        {
            // does this folderpath contain a .get folder
            if (System.IO.Directory.EnumerateDirectories(folderPath, ".git").Any())
            {
                yield return folderPath;
            }
            else
            {
                var folderName = System.IO.Path.GetFileName(folderPath);

                // !! NOTE !! we probably don't need these below lines
                //  It would be a situation where a non git repo php project was in there and it had these folders with sub git repos we don't care about in the vendor or node_modules
                // It's not for git repos inside git repos, if we find a git repo we don't traverse the folder further
                // prevent php composer vendor sub .git modules from interfering with things
                if (string.Equals(folderName, "vendor", StringComparison.OrdinalIgnoreCase) &&
                    System.IO.Directory.EnumerateFiles(folderPath, "composer.json").Any())
                {
                    continue;
                }

                // skip node_modules folders
                if (string.Equals(folderName, "node_modules", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                foreach (var subGit in getRepoPaths(folderPath))
                {
                    yield return subGit;
                }
            }
        }
    }
}