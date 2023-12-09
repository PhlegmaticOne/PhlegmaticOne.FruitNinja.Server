using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Sync;

public class GameDataMessageByScore : IGameDataSyncMessage
{
    private readonly int _targetScore;

    public GameDataMessageByScore(int targetScore)
    {
        _targetScore=targetScore;
    }

    public string BuildSyncMessage()
    {
        var message = ByScore();

        var json = JsonConvert
            .SerializeObject(message, typeof(GameDataBase), IGameDataSyncMessage.Settings);

        return "[GAMETYPE]" + json;
    }

    private GameDataBase ByScore()
    {
        return new GameDataByScore()
        {
            GameModeType = GameModeType.ByScore,
            SessionScore = _targetScore
        };
    }
}