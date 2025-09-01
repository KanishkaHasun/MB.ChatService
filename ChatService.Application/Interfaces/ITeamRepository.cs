using ChatService.Domain.Entities;
using ChatService.Domain.Enums;

namespace ChatService.Application.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetActiveTeamMembersByShiftAsync(IList<Shift> shifts);
        Task<IEnumerable<Team>> GetAllActiveAgentsByTeamAsync();
    }
}
