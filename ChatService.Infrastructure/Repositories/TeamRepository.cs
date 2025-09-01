using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Repositories
{
    internal class TeamRepository: ITeamRepository
    {
        private readonly MbDbContext _context;

        public TeamRepository(MbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetActiveTeamMembersByShiftAsync(IList<Shift> shifts)
        {
            return await _context.Teams
                .AsNoTracking()
                .Include(t => t.Agents.Where(a=> a.IsActive))
                .Where(t => shifts.Contains(t.Shift))
                .ToListAsync();
        }
        public async Task<IEnumerable<Team>> GetAllActiveAgentsByTeamAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .Include(t => t.Agents.Where(a=>a.IsActive))
                .ToListAsync();
        }
    }
}
