using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Server.Messages.Sync;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.GameData;

public class GameDataMessageByTime : IGameDataSyncMessage
{
    private readonly int _timeInSeconds;

    public GameDataMessageByTime(int timeInSeconds)
    {
        _timeInSeconds=timeInSeconds;
    }

    public string BuildSyncMessage()
    {
        var message = ByTime();

        var json = JsonConvert
            .SerializeObject(message, typeof(GameDataBase), IGameDataSyncMessage.Settings);

        return "[GAMETYPE]" + json;
    }

    private GameDataBase ByTime()
    {
        return new GameDataByTime()
        {
            GameModeType = GameModeType.ByTime,
            PlayTime = _timeInSeconds,
        };
    }
}
