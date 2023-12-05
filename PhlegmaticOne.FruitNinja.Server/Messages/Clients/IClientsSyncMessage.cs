namespace PhlegmaticOne.FruitNinja.Server.Messages.Sync;

public interface IClientsSyncMessage
{
    string BuildSyncMessage(int clientsCount);
}