using ChatService.Application.Models.Dtos;
using ChatService.Infrastructure.Constants;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Helpers
{
    internal static class PollingHelper
    {
        public static string GetPollingKey(Guid chatId)
        {
            return RedisKeyManagement.ChatPolling.Replace("{chatId}", chatId.ToString());
        }
        public static HashEntry[] MapToHashEntries(CreateChatResponseDto dto)
        {
            return new HashEntry[]
            {
            new HashEntry("userId", dto.UserId.ToString()),
            new HashEntry("agentId", dto.AgentId.ToString()),
            new HashEntry("status", dto.ChatStatus),
            new HashEntry("lastHeartbeat", DateTime.UtcNow.ToString())
            };
        }
        public static ChatStatusDto MapToChatStatusDto(HashEntry[] entry)
        {
            var map = entry.ToDictionary(
                entry => entry.Name.ToString(),
                entry => entry.Value.ToString()
                );
            return new ChatStatusDto
            {
                ChatStatus = map.GetValueOrDefault("status")?? "Unkonw",
                AgentId = Guid.TryParse(map.GetValueOrDefault("agentId"), out var agentId) ? agentId : Guid.Empty,
                UserId = Guid.TryParse(map.GetValueOrDefault("userId"), out var userId) ? userId : Guid.Empty,
            };
        }

    }
}
