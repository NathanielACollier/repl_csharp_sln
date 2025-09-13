namespace GitRepoFinder.repos;

public class GitRepoAnalysisThreadedRepo
{
    private bool stopRunning = false;
    private System.Threading.Thread myThread;
    
    public GitRepoAnalysisThreadedRepo()
    {
        
    }

    public void Stop()
    {
        this.stopRunning = true;
    }

    public void Start()
    {
        this.myThread = new System.Threading.Thread(() =>
        {
            runGitAnalysisLoopInThread();
        });
    }

    private void runGitAnalysisLoopInThread()
    {
        do
        {

        } while (this.stopRunning == false);
    }
}