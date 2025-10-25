namespace GitRepoFinder.models;

public class GitRepoAnalysisResultModel
{
    public string gitRepoPath { get; set; }
    public int uncommitedFileCount { get; set; }
    public string branchName { get; set; }
    public string remoteStatus { get; set; }
}