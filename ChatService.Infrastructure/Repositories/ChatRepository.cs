using ChatService.Application.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
