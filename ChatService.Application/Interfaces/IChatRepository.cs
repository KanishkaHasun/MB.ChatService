using ChatService.Domain.Entities;

namespace ChatService.Application.Interfaces
{
    public interface IChatRepository
    {
        Task AddAsync(ChatSession chat);
        Task<ChatSession?> GetByIdAsync(Guid chatId);
        Task<bool> AssignAgentAsync(Guid chatId, Guid agentId);
        Task<IList<ChatSession>> GetQueuedChatSessionsAsync();
    }
}
