using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatService.Background.Services
{
    public class QueueMonitorService : BackgroundService
    {
        private readonly ILogger<QueueMonitorService> _logger;

        public QueueMonitorService(ILogger<QueueMonitorService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queue Monitor Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
