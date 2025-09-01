using ChatService.Domain.Entities;
using ChatService.Domain.Enums;

namespace ChatService.Application.Interfaces
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAgentsByShiftAsync(Shift shift);
    }
}
