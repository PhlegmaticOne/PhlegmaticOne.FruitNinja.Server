namespace PhlegmaticOne.FruitNinja.Server.Log;

public class LoggerConsole : ILogger
{
    public bool IsVerbose { get; set; } = true;

    public void LogInfo(string message)
    {
        if (!IsVerbose)
        {
            return;
        }

        Console.WriteLine(message);
    }

    public void LogInfo(int playerId, string message)
    {
        if (!IsVerbose)
        {
            return;
        }

        LogImportantInfo(playerId, message);
    }

    public void LogImportantInfo(int playerId, string message)
    {
        Console.Write($"Player {playerId}: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void LogError(int playerId, string message)
    {
        if (!IsVerbose)
        {
            return;
        }

        Console.Write($"Player {playerId}: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}