using ChatService.Application.Interfaces;
using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;

namespace ChatService.BackgroundWorker.Services
{
    public class ShiftService : BackgroundService
    {
        private readonly ILogger<ShiftService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ShiftService(ILogger<ShiftService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Shift Service started.");

            await UpdateAgentsByShiftAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                //future work: i need to do a health check here
            }
        }

        private async Task UpdateAgentsByShiftAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var teamRepo = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
                var agentService = scope.ServiceProvider.GetRequiredService<IAgentService>();

                var teams = await teamRepo.GetAllActiveAgentsByTeamAsync();

                var agentDtos = teams.SelectMany(team =>
                    team.Agents.Select(agent =>
                        new AgentDto
                        {
                            AgentId = agent.Id,
                            Shift = team.Shift,
                            Seniority = agent.SeniorityLevel,
                            AvailableSlots = agent.Capacity
                        })).ToList();

                foreach (var agentDto in agentDtos)
                {
                    await agentService.AddAgentAsync(agentDto);
                }
            }
        }
    }
}
