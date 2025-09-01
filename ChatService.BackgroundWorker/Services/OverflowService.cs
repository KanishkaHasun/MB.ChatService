using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatService.Background.Services
{
    public class OverflowService : BackgroundService
    {
        private readonly ILogger<OverflowService> _logger;
        public OverflowService(ILogger<OverflowService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Overflow Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
