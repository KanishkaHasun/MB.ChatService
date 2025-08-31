using ChatService.Application.Models.Dtos;
using ChatService.Application.Services;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Constants;
using ChatService.Infrastructure.Helpers;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Services
{
    internal class ChatSessionPolllingService: IChatSessionPolllingService
    {
        private readonly IDatabase _database;

        public ChatSessionPolllingService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task AddAsync(CreateChatResponseDto createChatResponseDto)
        {
            string sessionKey = PollingHelper.GetPollingKey(createChatResponseDto.ChatId);
            var entries = PollingHelper.MapToHashEntries(createChatResponseDto);

           await  _database.HashSetAsync(sessionKey, entries);
        }
        public async Task<ChatStatusDto?> GetSessionStatusAsync(Guid chatId)
        {
            var key = PollingHelper.GetPollingKey(chatId);
            var entries = await _database.HashGetAllAsync(key);

            if (entries.Length == 0)
                return null;
            return PollingHelper.MapToChatStatusDto(entries);

        }

        public async Task UpdateAsync(CreateChatResponseDto dto)
        {
            var key = PollingHelper.GetPollingKey(dto.ChatId);
            var updatedEntries = PollingHelper.MapToHashEntries(dto);
            await _database.HashSetAsync(key, updatedEntries);
        }

        public async Task UpdateStatusAsync(Guid chatId, string newStatus)
        {
            var key = PollingHelper.GetPollingKey(chatId);
            await _database.HashSetAsync(key, new HashEntry[]
            {
                new HashEntry("status", newStatus)
            });
        }

        public async Task UpdateHeartbeatAsync(Guid chatId)
        {
            var key = PollingHelper.GetPollingKey(chatId);
            await _database.HashSetAsync(key, new HashEntry[]
            {
                new HashEntry("lastHeartbeat", DateTime.UtcNow.ToString())
            });
        }

        public async Task DeleteSessionAsync(Guid chatId, ChatStatus chatStatus)
        {
            string key = PollingHelper.GetPollingKey(chatId);
            await _database.HashSetAsync(key, new HashEntry[]
            {
                new HashEntry("status", chatStatus.ToString())
            });

            await _database.KeyExpireAsync(key, TimeSpan.FromMinutes(PollingConstants.ExpiryAfterDeleteMinutes));
        }
    }
}
