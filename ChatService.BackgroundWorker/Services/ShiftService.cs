using ChatService.Application.Interfaces;
using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.BackgroundWorker.Helpers;
using ChatService.Domain.Enums;

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
                await UpdateTeamCapacity();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
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

        private async Task UpdateTeamCapacity() 
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var teamRepo = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
                var teamManageService = scope.ServiceProvider.GetRequiredService<ITeamManagementService>();

                DateTime utcNow = DateTime.UtcNow;
                Shift shift = ShiftHelper.GetMainShift(utcNow);
                List<Shift> shiftsToUpdate = new List<Shift> { shift };

                if (ShiftHelper.IsOverflowTeamActive(utcNow)) 
                {
                    shiftsToUpdate.Add(Shift.OfficeHours);
                }

                var teams = await teamRepo.GetActiveTeamMembersByShiftAsync(shiftsToUpdate);

                int totalTeamCapacity = teams.Sum(t => t.MaxQueueSize);

                await teamManageService.AddTeamCapacity(totalTeamCapacity);

            }
        }
    }
}
