namespace GitRepoFinder.repos;

public static class log
{
    public static void Error(string message)
    {
        var previous = System.Console.ForegroundColor;
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"[{DateTime.Now}] - [ERROR] - {message}");
        System.Console.ForegroundColor = previous;
    }
}