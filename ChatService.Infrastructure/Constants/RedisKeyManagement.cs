namespace ChatService.Infrastructure.Constants
{
    public static class RedisKeyManagement
    {
         public static readonly string ChatQueueMain = "chat:queue:main";
        public static readonly string ChatPolling = "chat:session:{chatId}";
        //public static string ChatQueue => "chat:queue:main";
        //public static string TeamActive(string shift) => $"team:active:{shift}";
        //public static string TeamCapacity(string shift) => $"team:capacity:{shift}";
        //public static string AgentAvailability(string shift) => $"agent:availability:{shift}";
    }

}
