using PhlegmaticOne.FruitNinja.Server.Log;
using PhlegmaticOne.FruitNinja.Server.Messages.EndGame;
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

ILogger logger = new LoggerConsole();
IClientsSyncMessage clientsSyncMessage = new ClientsSyncMessage();
IEndGameSyncMessage endGameSyncMessage = new EndGameSyncMessage();
IGameDataSyncMessage gameDataSyncMessage = CreateMessage();

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

static IGameDataSyncMessage CreateMessage()
{
    Console.WriteLine("Enter game mode:");
    Console.WriteLine("0. By score");
    Console.WriteLine("1. By time");
    Console.WriteLine("2. By blocks");
    Console.WriteLine("3. By lifes");

    try
    {
        var mode = int.Parse(Console.ReadLine()!);
        var gameMode = (GameModeType)mode;

        switch (gameMode)
        {
            case GameModeType.ByTime:
                Console.WriteLine("Enter playing time (seconds): ");
                var timeInSeconds = int.Parse(Console.ReadLine()!);
                return new GameDataMessageByTime(timeInSeconds);
            case GameModeType.ByScore:
                Console.WriteLine("Enter score: ");
                var score = int.Parse(Console.ReadLine()!);
                return new GameDataMessageByScore(score);
            case GameModeType.ByBlocks:
                var blocks = new Dictionary<BlockTypeShared, int>();

                foreach (var block in Enum.GetValues<BlockTypeShared>())
                {
                    Console.WriteLine($"{(int)block}. - {block}");
                }

                Console.WriteLine("Enter blocks and counts. To stop any not number");

                try
                {
                    while (true)
                    {
                        Console.WriteLine("Enter block type: ");
                        var blockType = (BlockTypeShared)int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Enter count: ");
                        var count = int.Parse(Console.ReadLine()!);
                        blocks.Add(blockType, count);
                    }
                }
                catch
                {
                    Console.WriteLine("End of editing blocks and counts!");
                }

                return new GameDataMessageByBlocks(blocks);

            case GameModeType.ByLifes:
                Console.WriteLine("Enter lifes count: ");
                var lifesCount = int.Parse(Console.ReadLine()!);
                return new GameDataMessageByLifes(lifesCount);
        }
    }
    catch
    {
        Console.WriteLine("Return default game mode!");
    }

    return new GameDataMessageByScore(500);
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