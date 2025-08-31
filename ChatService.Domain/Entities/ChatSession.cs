using ChatService.Domain.Enums;

namespace ChatService.Domain.Entities
{
    public class ChatSession
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? AssignedAgentId { get; private set; }
        public DateTimeOffset DateTimeCreated { get; private set; } = DateTimeOffset.UtcNow;
        public ChatStatus Status { get; private set; }
        
        public User User { get; private set; }
        public Agent? AssignedAgent { get; private set; }

        private ChatSession()
        {
            User = new User();
        }
        public ChatSession(User user)
        {
            Id = Guid.NewGuid();
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserId = user.Id;
            Status = ChatStatus.INITIALIZED;
            DateTimeCreated = DateTime.UtcNow;
        }

        public void SetState(ChatStatus newState)
        {
            if (!IsValidTransition(Status, newState)) {
                throw new InvalidOperationException($"Invalid transition from {Status} to {newState}.");
            }
            Status = newState;
        }

        private bool IsValidTransition(ChatStatus current, ChatStatus next)
        {
            return current switch
            {
                ChatStatus.INITIALIZED => next == ChatStatus.QUEUED,
                ChatStatus.QUEUED => next == ChatStatus.IN_PROGRESS,
                ChatStatus.IN_PROGRESS => next == ChatStatus.COMPLETED,
                _ => false
            };
        }

    }
}
