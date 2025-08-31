using ChatService.Domain.Entities.Common;
using ChatService.Domain.Enums;

namespace ChatService.Domain.Entities
{
    public class Agent: BaseUser
    {
        public string Name { get; private set; } = string.Empty;
        public SeniorityLevel SeniorityLevel { get; private set; }
    }
}
