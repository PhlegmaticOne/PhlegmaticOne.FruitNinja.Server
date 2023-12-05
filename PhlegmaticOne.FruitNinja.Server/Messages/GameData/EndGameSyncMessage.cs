using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.GameData
{
    public class EndGameSyncMessage : IEndGameSyncMessage
    {
        public string BuildSyncMessage(PlayersSyncMessage playersSyncMessage)
        {
            var json = JsonConvert.SerializeObject(playersSyncMessage);
            return "[ENDSYNC]" + json;
        }
    }
}
