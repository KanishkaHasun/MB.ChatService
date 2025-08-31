using ChatService.Domain.Entities.Common;
using System;

namespace ChatService.Domain.Entities
{
    public class User: BaseUser
    {
        public User(string? username = null) : base(username)
        {
        }

        private readonly List<ChatSession> _chats = new();
        public IReadOnlyCollection<ChatSession> Chats => _chats.AsReadOnly();

        public ChatSession CreateChat()
        {
            var chat = new ChatSession(this);
            _chats.Add(chat);
            return chat;
        }

    }
}
