using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Helpers;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Services
{
    internal class AgentService: IAgentService
    {
        private readonly IDatabase _database;

        public AgentService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task AddAgentAsync(AgentDto agent)
        {
            string key = AgentHelper.GetAgentKey(agent.AgentId);
            string sortedAgentsKey = AgentHelper.GetSortedAgentsKey((int)agent.Shift);

            await _database.HashSetAsync(key, AgentHelper.MapToHashEntries(agent));
            await _database.SortedSetAddAsync(sortedAgentsKey, agent.AgentId.ToString(), (int)agent.Seniority);
        }

        public async Task RemoveAgentAsync(AgentDto agent)
        {
            string key = AgentHelper.GetAgentKey(agent.AgentId);
            string sortedAgentsKey = AgentHelper.GetSortedAgentsKey((int)agent.Shift);

            await _database.KeyDeleteAsync(key);
            await _database.SortedSetRemoveAsync(sortedAgentsKey, agent.AgentId.ToString());
        }

        public async Task<AgentDto?> GetBestAvailableAgentAsync(Shift shift)
        {
            string sortedAgentsKey = AgentHelper.GetSortedAgentsKey((int)shift);

            var sortedAgents = await _database.SortedSetRangeByRankAsync(sortedAgentsKey, 0, -1, Order.Ascending);

            foreach (var agentIdValue in sortedAgents)
            {
                string agentKey = AgentHelper.GetAgentKey(Guid.Parse(agentIdValue!));
                var entries = await _database.HashGetAllAsync(agentKey);

                if (entries.Length == 0) continue;

                var dict = entries.ToDictionary(h => h.Name.ToString(), h => h.Value.ToString());
                int availableSlots = int.Parse(dict["availableSlots"]);

                if (availableSlots > 0)
                {
                    return new AgentDto
                    {
                        AgentId = Guid.Parse(dict["agentId"]),
                        Seniority = (SeniorityLevel) Int16.Parse(dict["seniority"]),
                        AvailableSlots = availableSlots
                    };
                }
            }

            return null;
        }
        public async Task UpdateAvailableSlotsAsync(AgentDto agent, bool isDecrement, int count)
        {
            string key = AgentHelper.GetAgentKey(agent.AgentId);
            
            if (isDecrement)
            {
                await _database.HashDecrementAsync(key, "availableSlots", count);
                return;
            }
            await _database.HashIncrementAsync(key, "availableSlots", count);
        }
    }
}
