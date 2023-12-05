using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.GameData
{
    public interface IEndGameSyncMessage
    {
        string BuildSyncMessage(PlayersSyncMessage playersSyncMessage);
    }
}
