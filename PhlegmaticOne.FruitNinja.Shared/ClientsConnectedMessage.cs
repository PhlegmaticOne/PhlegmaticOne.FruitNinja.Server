using System;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Clients
{
    [Serializable]
    public class ClientsConnectedMessage
    {
        public int ClientsConnected { get; set; }
    }
}