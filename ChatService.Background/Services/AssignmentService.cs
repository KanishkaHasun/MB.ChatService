using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatService.Background.Services
{
    public class AssignmentService : BackgroundService
    {
        private readonly ILogger<AssignmentService> _logger;
        public AssignmentService(ILogger<AssignmentService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Assignment Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
