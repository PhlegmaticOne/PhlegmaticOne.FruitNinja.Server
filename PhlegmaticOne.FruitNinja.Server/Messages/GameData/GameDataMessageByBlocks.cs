using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Server.Messages.Sync;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.GameData;

public class GameDataMessageByBlocks : IGameDataSyncMessage
{
    private readonly Dictionary<BlockTypeShared, int> _blocks;

    public GameDataMessageByBlocks(Dictionary<BlockTypeShared, int> blocks)
    {
        _blocks=blocks;
    }

    public string BuildSyncMessage()
    {
        var message = ByBlocks();

        var json = JsonConvert
            .SerializeObject(message, typeof(GameDataBase), IGameDataSyncMessage.Settings);

        return "[GAMETYPE]" + json;
    }

    private GameDataBase ByBlocks()
    {
        return new GameDataByBlocks()
        {
            GameModeType = GameModeType.ByScore,
            BlocksData = _blocks
        };
    }
}
