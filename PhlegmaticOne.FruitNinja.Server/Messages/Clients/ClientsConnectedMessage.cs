using Newtonsoft.Json;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Clients;

[Serializable]
public class ClientsConnectedMessage
{
    [JsonConstructor]
    public ClientsConnectedMessage(int clientsConnected)
    {
        ClientsConnected = clientsConnected;
    }

    public int ClientsConnected { get; }
}