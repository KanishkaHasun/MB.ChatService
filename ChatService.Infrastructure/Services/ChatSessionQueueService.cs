using ChatService.Application.Services;
using ChatService.Infrastructure.Constants;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Services
{
    internal class ChatSessionQueueService: IChatSessionQueueService
    {
        private readonly IDatabase _database;

        public ChatSessionQueueService(IConnectionMultiplexer redis) 
        { 
            _database= redis.GetDatabase();
        }

        public async Task EnqueueAsync(string chatId)
        {
            var queueKey = RedisKeyManagement.ChatQueue;
            await _database.ListRightPushAsync(queueKey, chatId);
        }

        public async Task<string?> DequeueAsync()
        {
            var queueKey = RedisKeyManagement.ChatQueue;
            return await _database.ListLeftPopAsync(queueKey);
        }

        public async Task<int> GetQueueLengthAsync()
        {
            var queueKey = RedisKeyManagement.ChatQueue;
            return (int)await _database.ListLengthAsync(queueKey);
        }
        public async Task<string?> PeekAsync()
        {
            var queueKey = RedisKeyManagement.ChatQueue;
            return await _database.ListGetByIndexAsync(queueKey, 0);
        }
        public async Task RemoveAsync(Guid chatId)
        {
            var queueKey = RedisKeyManagement.ChatQueue;
            await _database.ListRemoveAsync(queueKey, chatId.ToString(), 0);
        }
    }
}
