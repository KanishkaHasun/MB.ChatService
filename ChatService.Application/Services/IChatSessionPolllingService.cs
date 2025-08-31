using ChatService.Application.Models.Dtos;
using ChatService.Domain.Enums;

namespace ChatService.Application.Services
{
    public interface IChatSessionPolllingService
    {
        Task AddAsync(CreateChatResponseDto createChatResponseDto);
        Task<ChatStatusDto?> GetSessionStatusAsync(Guid chatId);
        Task UpdateAsync(CreateChatResponseDto dto);
        Task UpdateStatusAsync(Guid chatId, string newStatus);
        Task UpdateHeartbeatAsync(Guid chatId);
        Task DeleteSessionAsync(Guid chatId, ChatStatus chatStatus);

    }
}
