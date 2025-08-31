namespace ChatService.Application.Services
{
    public interface IQueueService
    {
        Task EnqueueAsync(string queueKey, string value);
        Task<string?> DequeueAsync(string queueKey);
        Task<int> GetQueueLengthAsync(string queueKey);

    }
}
