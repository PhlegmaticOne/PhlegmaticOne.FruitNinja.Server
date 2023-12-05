namespace PhlegmaticOne.FruitNinja.Server.Session;

public interface IGameSession : IDisposable
{
    void Start(GameSessionConfiguration configuration);
    Task PerformAsync();
}