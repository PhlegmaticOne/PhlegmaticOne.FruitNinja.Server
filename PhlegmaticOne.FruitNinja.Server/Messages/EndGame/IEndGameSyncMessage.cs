using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Messages.EndGame
{
    public interface IEndGameSyncMessage
    {
        string BuildSyncMessage(PlayersSyncMessage playersSyncMessage);
    }
}
