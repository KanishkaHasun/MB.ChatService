using ChatService.Application.Interfaces;
using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.BackgroundWorker.Helpers;
using ChatService.Domain.Enums;

namespace ChatService.Background.Services
{
    public class AssignmentService : BackgroundService
    {
        private readonly ILogger<AssignmentService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public AssignmentService(ILogger<AssignmentService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Assignment Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await AssignAgentToChatSession();
                await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
            }
        }

        private async Task AssignAgentToChatSession()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var teamRepo = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
                var chatRepo = scope.ServiceProvider.GetRequiredService<IChatRepository>();

                var agentService = scope.ServiceProvider.GetRequiredService<IAgentService>();
                var chatService = scope.ServiceProvider.GetRequiredService<IChatSessionQueueService>();

                DateTime utcNow = DateTime.UtcNow;
                string? chatSessionId = await SelectOldestWaitingChat(chatService);

                if (string.IsNullOrEmpty(chatSessionId))
                {
                    return;
                }

                AgentDto? agent = await SelectBestAvailableAgent(agentService, utcNow);

                if (agent is null)
                {
                    return;
                }

                bool isAssigned = await chatRepo.AssignAgentAsync(Guid.Parse(chatSessionId), agent.AgentId);

                while (!isAssigned)
                {
                    _logger.LogError($"Failed to assign agent {agent.AgentId} to chat session {chatSessionId}");
                    await RemoveAgentFromQueue(agentService, agent);
                    await AssignAgentToChatSession();
                    await Task.Delay(TimeSpan.FromMilliseconds(10));
                }

                await RemoveChatSessionFromQueue(chatService);
                await UpdateAgentCapacity(agentService, agent);

            }
        }

        private static async Task<string?> SelectOldestWaitingChat(IChatSessionQueueService chatService)
        {
            return await chatService.PeekAsync();
        }
        private static async Task<AgentDto?> SelectBestAvailableAgent(IAgentService agentService, DateTime utcNow)
        {
            Shift shift = ShiftHelper.GetMainShift(utcNow);

            AgentDto? agent = await agentService.GetBestAvailableAgentAsync(shift);

            if (agent is null && ShiftHelper.IsOverflowTeamActive(utcNow))
            {
                agent = await agentService.GetBestAvailableAgentAsync(shift);
            }
            return agent;
        }

        private static async Task RemoveChatSessionFromQueue(IChatSessionQueueService chatService)
        {
            await chatService.DequeueAsync();
        }

        private static async Task UpdateAgentCapacity(IAgentService agentService, AgentDto agent)
        {
            int soltCount = 1;
            await agentService.UpdateAvailableSlotsAsync(agent, true, soltCount);
        }

        private static async Task RemoveAgentFromQueue(IAgentService agentService, AgentDto agent)
        {
            await agentService.RemoveAgentAsync(agent);
        }
    }
}
