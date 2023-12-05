namespace PhlegmaticOne.FruitNinja.Server.Log;

public interface ILogger
{
    bool IsVerbose { get; set; }
    void LogInfo(string message);
    void LogInfo(int playerId, string message);
    void LogImportantInfo(int playerId, string message);
    void LogError(int playerId, string message);
}