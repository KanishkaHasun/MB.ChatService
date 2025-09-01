using ChatService.Application.Models.Dtos;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Constants;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Helpers
{
    internal class AgentHelper
    {
        public static string GetAgentKey(Guid agentId)
        {
            return RedisKeyManagement.AgentKey.Replace("{agentId}", agentId.ToString());
        }

        public static string GetSortedAgentsKey(int shift)
        {
            return RedisKeyManagement.SortedAgentsKey.Replace("{shift}", shift.ToString());
        }

        public static HashEntry[] MapToHashEntries(AgentDto agent)
        {
           return new HashEntry[]
            {
                new HashEntry("agentId", agent.AgentId.ToString()),
                new HashEntry("seniority", (int)agent.Seniority),
                new HashEntry("availableSlots", agent.AvailableSlots),
            };
        }

        public static AgentDto MapToAgentDto(HashEntry[] entries)
        {
            var dict = entries.ToDictionary(e => e.Name.ToString(), e => e.Value.ToString());
            return new AgentDto
            {
                AgentId = Guid.Parse(dict["agentId"]),
                Seniority = (SeniorityLevel)int.Parse(dict["seniority"]),
                AvailableSlots = int.Parse(dict["availableSlots"])
            };
        }
    }
}
