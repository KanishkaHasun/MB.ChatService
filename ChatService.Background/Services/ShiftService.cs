using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatService.Background.Services
{

    public class ShiftService : BackgroundService
    {
        private readonly ILogger<ShiftService> _logger;

        public ShiftService(ILogger<ShiftService> logger)
        {
            _logger = logger; ;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Shift Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}