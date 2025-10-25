using LibGit2Sharp;

namespace GitRepoFinder.repos;

public class GitOperationsAnalysisRepo
{
    public static Task<models.GitRepoAnalysisResultModel> GetStatusInfo(string gitRepoPath)
    {
        return Task.Run(() =>
        {
            var result = new models.GitRepoAnalysisResultModel
            {
                gitRepoPath = gitRepoPath
            };
            
            // Create a new instance of the LibGit2Sharp repository class
            using (var repo = new LibGit2Sharp.Repository(gitRepoPath))
            {
                // Check if there are any uncommitted changes in the repository
                var status = repo.RetrieveStatus(new StatusOptions
                {

                });
                bool hasUncommitedChanges = status.IsDirty;

                // Get the number of files with changes (uncommitted)
                int fileCount = status.Added.Count() +
                                status.Modified.Count() +
                                status.Untracked.Count();

                result.uncommitedFileCount = fileCount;
                result.branchName = repo.Head.FriendlyName;
            }

            return result;
        });
    }
}