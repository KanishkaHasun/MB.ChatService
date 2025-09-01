using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Domain.Enums;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
       
        public async Task<bool> AssignAgentAsync(Guid chatId, Guid agentId)
        {
            ChatSession? chatSession = await _context.ChatSessions.FindAsync(chatId);
            if (chatSession == null)
            {
                return false;
            }
            chatSession.SetAgent(agentId);
            chatSession.SetState(ChatStatus.ASSIGNED);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<ChatSession>> GetQueuedChatSessionsAsync()
        {
            return await _context.ChatSessions
                .Where(c => c.Status == ChatStatus.QUEUED).ToListAsync();
        }
    }
}
