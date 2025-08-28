namespace ChatService.Domain.Entities
{
    public class ChatSession
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? AssignedAgentId { get; private set; }
        public int PollCount { get; private set; } = 0;
        public bool IsActive { get; private set; } = true;
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    }
}
