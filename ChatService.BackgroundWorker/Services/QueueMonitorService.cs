using ChatService.Application.Interfaces;
using ChatService.Application.Services;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Repositories;

namespace ChatService.Background.Services
{
    public class QueueMonitorService : BackgroundService
    {
        private readonly ILogger<QueueMonitorService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public QueueMonitorService(ILogger<QueueMonitorService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queue Monitor Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await HandleInactiveChats();
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        private async Task HandleInactiveChats()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var chatSessionRepo = scope.ServiceProvider.GetRequiredService<IChatRepository>();
                var pollingService = scope.ServiceProvider.GetRequiredService<IChatSessionPolllingService>();
                var chatService = scope.ServiceProvider.GetRequiredService<IChatSessionQueueService>();
                var repoManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();

                var queuedChats = await chatSessionRepo.GetQueuedChatSessionsAsync();

                var chatIds = queuedChats
                    .Select(chat => chat.Id).ToList();
                TimeSpan inactivityThreshold = TimeSpan.FromSeconds(3);
               
                IList<Guid> inactiveChats = await pollingService.ExpireInactiveChatsAsync(chatIds, inactivityThreshold);

                foreach (var inactiveChatId in inactiveChats)
                {
                    await chatService.RemoveAsync(inactiveChatId);
                    var chat = queuedChats.FirstOrDefault(c => c.Id == inactiveChatId);
                    if (chat != null)
                    {
                        chat.SetState(ChatStatus.ABANDONED);
                    }
                }
                await repoManager.SaveChangesAsync();
            }
        }
    }
}
