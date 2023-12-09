using System.Text;
using Newtonsoft.Json;
using PhlegmaticOne.FruitNinja.Server.Log;
using PhlegmaticOne.FruitNinja.Server.Messages.EndGame;
using PhlegmaticOne.FruitNinja.Server.Messages.Sync;
using PhlegmaticOne.FruitNinja.Shared;

namespace PhlegmaticOne.FruitNinja.Server.Session;

public class GameSession : IGameSession
{
    private static readonly byte[] EndGame = Encoding.UTF8.GetBytes("[ENDGAME]");

    private readonly ILogger _logger;
    private readonly IClientsSyncMessage _clientsSyncMessage;
    private readonly IGameDataSyncMessage _gameDataSyncMessage;
    private readonly IEndGameSyncMessage _endGameSyncMessage;
    private readonly List<int> _players;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly PlayersSyncMessage _playersSyncMessage;

    private GameSessionConfiguration _configuration = null!;
    private Telepathy.Server _server = null!;

    public GameSession(ILogger logger, 
        IClientsSyncMessage clientsSyncMessage,
        IGameDataSyncMessage syncMessage,
        IEndGameSyncMessage endGameSyncMessage)
    {
        _logger = logger;
        _clientsSyncMessage=clientsSyncMessage;
        _gameDataSyncMessage = syncMessage;
        _endGameSyncMessage=endGameSyncMessage;
        _players = new List<int>();
        _playersSyncMessage = new PlayersSyncMessage();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Start(GameSessionConfiguration configuration)
    {
        _configuration = configuration;
        _logger.IsVerbose = configuration.Verbose;
        _server = new Telepathy.Server(configuration.MaxMessageSize);
        _server.OnConnected += OnPlayerConnected;
        _server.OnData += OnPlayerMessageReceived;
        _server.OnDisconnected += OnPlayerDisconnected;
        _server.Start(configuration.Port);
        _logger.LogInfo("Server started successfully!");
    }

    public Task PerformAsync()
    {
        var updateInterval = 1000 / _configuration.Fps;
        return Task.Run(async () =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(updateInterval);
                _server.Tick(20);
            }
        }, _cancellationTokenSource.Token);
    }

    public void Dispose()
    {
        _server.OnConnected -= OnPlayerConnected;
        _server.OnData -= OnPlayerMessageReceived;
        _server.OnDisconnected -= OnPlayerDisconnected;
        _server.Stop();
    }

    private void OnPlayerConnected(int playerId)
    {
        _logger.LogImportantInfo(playerId, "Client connected!");
        _players.Add(playerId);
        BroadcastMessage(_clientsSyncMessage.BuildSyncMessage(_players.Count));

        if(_players.Count == _configuration.ClientsCount)
        {
            BroadcastMessage(_gameDataSyncMessage.BuildSyncMessage());
        }
    }

    private void OnPlayerMessageReceived(int playerId, ArraySegment<byte> message)
    {
        if (IsMessageOfType(message, EndGame))
        {
            var endGameData = ParseObjectFromMessage<PlayerEndGameMessage>(message, EndGame.Length);
            _playersSyncMessage.Add(endGameData);

            if(_playersSyncMessage.IsFull()) 
            {
                BroadcastMessage(_endGameSyncMessage.BuildSyncMessage(_playersSyncMessage));
            }
        }

        if (_configuration.IsEchoPlay)
        {
            _server.Send(playerId, message);
            return;
        }

        foreach (var player in _players)
        {
            if (player != playerId)
            {
                _server.Send(player, message);
            }
        }
    }

    private void OnPlayerDisconnected(int playerId)
    {
        _logger.LogImportantInfo(playerId, "Client disconnected");
        _players.Remove(playerId);

        if (_players.Count == 0)
        {
            _cancellationTokenSource.Cancel();
        }
    }

    private void BroadcastMessage(string message)
    {
        var segment = Encoding.UTF8.GetBytes(message);

        foreach (var player in _players)
        {
            _server.Send(player, segment);
        }
    }

    private static T ParseObjectFromMessage<T>(ArraySegment<byte> message, int start) where T : class
    {
        var messageBytes = message[new Range(new Index(start), new Index(0, true))];
        var messageJson = Encoding.UTF8.GetString(messageBytes);
        return JsonConvert.DeserializeObject<T>(messageJson)!;
    }

    private static bool IsMessageOfType(ArraySegment<byte> message, byte[] messageType)
    {
        for (var i = 0; i < messageType.Length; i++)
        {
            if (messageType[i] != message[i])
            {
                return false;
            }
        }

        return true;
    }
}