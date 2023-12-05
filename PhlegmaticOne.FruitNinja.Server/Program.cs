using PhlegmaticOne.FruitNinja.Server.Log;
using PhlegmaticOne.FruitNinja.Server.Messages.GameData;
using PhlegmaticOne.FruitNinja.Server.Messages.Sync;
using PhlegmaticOne.FruitNinja.Server.Session;
using PhlegmaticOne.FruitNinja.Shared;

const int port = 8888;
const int maxMessageSize = 16384;
const int clientsCount = 2;
const int fps = 60;
const bool verbose = false;
const bool isEchoPlay = false;

Console.WriteLine("Enter game mode:");
Console.WriteLine("0. By score");
Console.WriteLine("2. By blocks");

var mode = int.Parse(Console.ReadLine()!);
var gameMode = (GameModeType)mode;

ILogger logger = new LoggerConsole();
IClientsSyncMessage clientsSyncMessage = new ClientsSyncMessage();
IGameDataSyncMessage gameDataSyncMessage = new GameDataSyncMessage(gameMode);
IEndGameSyncMessage endGameSyncMessage = new EndGameSyncMessage();

var configuration = new GameSessionConfiguration(port, maxMessageSize, clientsCount, fps, verbose, isEchoPlay);


while (true)
{
    using (IGameSession gameSession =
        new GameSession(logger, clientsSyncMessage, gameDataSyncMessage, endGameSyncMessage))
    {
        Console.WriteLine("Staring new session!");
        gameSession.Start(configuration);
        await gameSession.PerformAsync();
    }

    if (CanStartNewSession() == false)
    {
        break;
    }
}

static bool CanStartNewSession()
{
    Console.WriteLine("Start new session?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("_. No");

    try
    {
        var choice = int.Parse(Console.ReadLine()!);

        if (choice != 1)
        {
            return false;
        }
    }
    catch
    {
        return false;
    }

    return true;
}