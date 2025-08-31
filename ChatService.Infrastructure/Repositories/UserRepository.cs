using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Data;

namespace ChatService.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly MbDbContext _context;
        public UserRepository(MbDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
             await _context.Users.AddAsync(user);
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
