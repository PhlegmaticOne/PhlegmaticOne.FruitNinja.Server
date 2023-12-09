using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Server.Messages.Clients;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Sync;

public class ClientsSyncMessage : IClientsSyncMessage
{
    public string BuildSyncMessage(int clientsCount)
    {
        var message = new ClientsConnectedMessage
        {
            ClientsConnected = clientsCount
        };

        var json = JsonConvert.SerializeObject(message);
        return "[SYNC]" + json;
    }
}