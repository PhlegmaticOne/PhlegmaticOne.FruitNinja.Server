namespace PhlegmaticOne.FruitNinja.Server.Session;

public class GameSessionConfiguration
{
    public int Port { get; }
    public int MaxMessageSize { get; }
    public int ClientsCount { get; }
    public int Fps { get; }
    public bool Verbose { get; }
    public bool IsEchoPlay { get; }

    public GameSessionConfiguration(int port, int maxMessageSize, int clientsCount, int fps, bool verbose, bool isEchoPlay)
    {
        Port = port;
        MaxMessageSize = maxMessageSize;
        ClientsCount = clientsCount;
        Fps = fps;
        Verbose = verbose;
        IsEchoPlay = isEchoPlay;
    }
}