using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Repositories
{
    internal class AgentRepository: IAgentRepository
    {
        private readonly MbDbContext _context;

        public AgentRepository(MbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agent>> GetAgentsByShiftAsync(Shift shift)
        {
            return await _context.Agents
                .Include(a => a.Team)
                .Where(a => a.Team != null && a.Team.Shift == shift)
                .ToListAsync();
        }
    }
}
