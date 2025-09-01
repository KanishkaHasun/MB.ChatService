using ChatService.Application.Models.Dtos;
using ChatService.Domain.Enums;

namespace ChatService.Application.Services
{
    public interface IAgentService
    {
        Task AddAgentAsync(AgentDto agent);
        Task RemoveAgentAsync(AgentDto agent);
        Task<AgentDto?> GetBestAvailableAgentAsync(Shift shift);
        Task UpdateAvailableSlotsAsync(AgentDto agent, bool isDecrement, int count);
    }
}
