using ChatService.Application.Models.Dtos;

namespace ChatService.Application.Services
{
    public interface IAgentService
    {
        Task AddAgentAsync(AgentDto agent);
        Task RemoveAgentAsync(AgentDto agent);
    }
}
