namespace ChatService.Application.Services
{
    public interface IChatSessionQueueService
    {
        Task EnqueueAsync(string chatId);
        Task<string?> DequeueAsync();
        Task<int> GetQueueLengthAsync();
        Task<string?> PeekAsync();
        Task RemoveAsync(Guid chatId);

    }
}
