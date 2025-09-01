using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
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
    }
}
