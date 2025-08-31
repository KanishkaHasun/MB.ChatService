using ChatService.Application.Interfaces;
using ChatService.Infrastructure.Data;

namespace ChatService.Infrastructure.Repositories
{
   
    public class RepositoryManager: IRepositoryManager
    {
        private readonly MbDbContext _context;

        public RepositoryManager(MbDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
