namespace ChatService.Infrastructure.Constants
{
    public static class RedisKeyManagement
    {
        public static readonly string ChatQueue = "chat:queue";
        public static readonly string ChatPolling = "chat:session:{chatId}";
        public static readonly string AgentKey = "team:agent:{agentId}";
        public static readonly string SortedAgentsKey = "team:agents:{shift}";
        public static readonly string MaxTeamCapacity = "team:capacity";
    }

}
