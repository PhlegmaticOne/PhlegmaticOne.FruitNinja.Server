using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Sync;

public class GameDataSyncMessage : IGameDataSyncMessage
{
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    private readonly GameModeType modeType;

    public GameDataSyncMessage(GameModeType modeType)
    {
        this.modeType=modeType;
    }

    public string BuildSyncMessage()
    {
        var message = modeType switch
        {
            GameModeType.ByScore => ByScore(),
            GameModeType.ByTime => throw new NotImplementedException(),
            GameModeType.ByBlocks => ByBlocks(),
        };

        var json = JsonConvert.SerializeObject(message, typeof(GameDataBase), Settings);
        return "[GAMETYPE]" + json;
    }

    private GameDataBase ByScore()
    {
        return new GameDataByScore()
        {
            GameModeType = GameModeType.ByScore,
            SessionScore = 100
        };
    }

    private GameDataBase ByBlocks()
    {
        return new GameDataByBlocks()
        {
            GameModeType = GameModeType.ByBlocks,
            BlocksData = new Dictionary<BlockTypeShared, int>
            {
                { BlockTypeShared.Mango, 1 },
            }
        };
    }
}