using Newtonsoft.Json;

namespace PhlegmaticOne.FruitNinja.Server.Messages.Sync;

public interface IGameDataSyncMessage
{
    string BuildSyncMessage();

    static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
}