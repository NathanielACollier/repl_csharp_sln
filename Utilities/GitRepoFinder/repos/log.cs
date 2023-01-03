namespace GitRepoFinder.repos;

public static class log
{
    private static void writeToConsole(string level, string message, System.ConsoleColor color)
    {
        var previous = System.Console.ForegroundColor;
        System.Console.ForegroundColor = color;
        System.Console.WriteLine($"[{DateTime.Now}] - [{level}] - {message}");
        System.Console.ForegroundColor = previous; 
    }


    private static void writeToFile(string level, string message)
    {
        string logFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, $"log{DateTime.Now:yyyyMMdd}.txt");
        
        using (FileStream fs = System.IO.File.Open(logFilePath,
                   mode: System.IO.FileMode.Append,
                   access: System.IO.FileAccess.Write,
                   share: System.IO.FileShare.ReadWrite | System.IO.FileShare.Delete)) {
            using( var writer = new StreamWriter(fs))
            {
                string entry = $"[{DateTime.Now}] - [{level}] - {message}";
                writer.WriteLine(entry);
            }
        }
    }
    
    public static void Error(string message)
    {
        writeToConsole("ERROR", message, ConsoleColor.Red);
        writeToFile("ERROR", message);
    }


    public static void Info(string message)
    {
        writeToConsole("INFO", message, System.Console.ForegroundColor);
        writeToFile("INFO", message);
    }
}