using ChatService.Domain.Enums;

namespace ChatService.Domain.Entities
{
    public class Agent
    {
        public Guid Id { get;private set; }
        public string Name { get; private set; } = string.Empty;
        public SeniorityLevel SeniorityLevel { get; private set; }
    }
}
