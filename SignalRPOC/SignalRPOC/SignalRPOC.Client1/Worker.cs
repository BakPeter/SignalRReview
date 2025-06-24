using System.Text.Json;
using SignalRPOC.Commons.Interfaces;
using SignalRPOC.Commons.Types;

namespace SignalRPOC.Client1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISignalRImporter _importer;

        public Worker(ILogger<Worker> logger, ISignalRImporter importer)
        {
            _logger = logger;

            _importer = importer;
            _importer.OnMessage += OnMessage;
        }

        private void OnMessage(object? sender, MessageModel message)
        {
            _logger.LogInformation("Message Received: {msgTime} {user} {payload}", message.Time.ToLongTimeString(),
                message.User,
                JsonSerializer.Serialize(message.Payload));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _importer.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    var message = new MessageModel
                    {
                        Time = DateTime.Now, User = "Client 1",
                        Payload = new Client1Message { Lat = 1.1, Lon = 2.2, Name = "House" }
                    };
                    // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation("Message Send: {message}", JsonSerializer.Serialize(message));
                    _importer.SendMessageAsync(message);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}