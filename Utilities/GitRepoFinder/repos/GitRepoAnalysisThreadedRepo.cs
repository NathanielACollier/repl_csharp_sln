namespace GitRepoFinder.repos;

public class GitRepoAnalysisThreadedRepo
{
    private bool stopRunning = false;
    private System.Threading.Thread myThread;
    private List<string> gitRepoList = new();

    public event EventHandler<models.GitRepoAnalysisResultModel> onGitRepoAnalysisFinished;
    
    public GitRepoAnalysisThreadedRepo()
    {
        
    }

    public void Stop()
    {
        this.stopRunning = true;
    }

    public void Start()
    {
        this.myThread = new System.Threading.Thread(async () =>
        {
            await runGitAnalysisLoopInThread();
        });
    }

    private async Task runGitAnalysisLoopInThread()
    {
        do
        {
            foreach (string gitRepoPath in this.gitRepoList ?? new())
            {
                if (this.stopRunning)
                {
                    break; // break out of loop if we need to stop running
                }

                await ProcessGitRepo(gitRepoPath);
            }
        } while (this.stopRunning == false);
    }

    private async Task ProcessGitRepo(string gitRepoPath)
    {
        var analysisResults = await repos.GitOperationsAnalysisRepo.GetStatusInfo(gitRepoPath);

        this.onGitRepoAnalysisFinished?.Invoke(this, analysisResults);
    }
}