using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Server.Messages.Sync;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.GameData
{
    public class GameDataMessageByLifes : IGameDataSyncMessage
    {
        private readonly int _lifesCount;

        public GameDataMessageByLifes(int lifesCount)
        {
            _lifesCount=lifesCount;
        }

        public string BuildSyncMessage()
        {
            var message = ByLifes();

            var json = JsonConvert
                .SerializeObject(message, typeof(GameDataBase), IGameDataSyncMessage.Settings);

            return "[GAMETYPE]" + json;
        }

        private GameDataBase ByLifes()
        {
            return new GameDataByLifes()
            {
                GameModeType = GameModeType.ByLifes,
                LifesCount = _lifesCount
            };
        }
    }
}
