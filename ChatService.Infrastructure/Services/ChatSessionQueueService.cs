using ChatService.Application.Services;
using ChatService.Infrastructure.Constants;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Services
{
    public class ChatSessionQueueService: IChatSessionQueueService
    {
        private readonly IDatabase _database;

        public ChatSessionQueueService(IConnectionMultiplexer redis) 
        { 
            _database= redis.GetDatabase();
        }

        public async Task EnqueueAsync(string chatId)
        {
            var queueKey = RedisKeyManagement.ChatQueueMain;
            await _database.ListRightPushAsync(queueKey, chatId);
        }

        public async Task<string?> DequeueAsync()
        {
            var queueKey = RedisKeyManagement.ChatQueueMain;
            return await _database.ListLeftPopAsync(queueKey);
        }

        public async Task<int> GetQueueLengthAsync()
        {
            var queueKey = RedisKeyManagement.ChatQueueMain;
            return (int)await _database.ListLengthAsync(queueKey);
        }

    }
}
