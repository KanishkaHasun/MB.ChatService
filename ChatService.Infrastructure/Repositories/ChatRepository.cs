using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Data;

namespace ChatService.Infrastructure.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private readonly MbDbContext _context;
        public ChatRepository(MbDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ChatSession chat)
        {
           await _context.ChatSessions.AddAsync(chat);
        }
        public async Task<ChatSession?> GetByIdAsync(Guid chatId)
        {
           return await _context.ChatSessions.FindAsync(chatId);
        }
    }
}
