using ChatService.Application.Services;
using ChatService.Infrastructure.Constants;
using StackExchange.Redis;

namespace ChatService.Infrastructure.Services
{
    internal class TeamManagementService: ITeamManagementService
    {
        private readonly IDatabase _database;
        public TeamManagementService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task AddTeamCapacity(int maxTeamCapacity)
        {
            string key = RedisKeyManagement.MaxTeamCapacity;
            await _database.StringSetAsync(key, maxTeamCapacity);
        }
        public async Task<int> GetMaxTeamCapacity()
        {
            string key = RedisKeyManagement.MaxTeamCapacity;
            var capacity = await _database.StringGetAsync(key);
           return (capacity.IsNullOrEmpty ? 0: (int)capacity);
        }
    }
}
