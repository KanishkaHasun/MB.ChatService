using ChatService.Domain.Constants;
using ChatService.Domain.Entities.Common;
using ChatService.Domain.Enums;

namespace ChatService.Domain.Entities
{
    public class Agent: BaseUser
    {
        public string Name { get; private set; } = string.Empty;
        public SeniorityLevel SeniorityLevel { get; private set; }
        public bool IsActive { get; private set; }
        public int TeamId { get; private set; }
        public Team Team { get; private set; } = null!;

        protected Agent() { 
        }

        public Agent(string username, string name, SeniorityLevel seniority, int teamId, bool isActive = true)
            : base(username)
        {
            Name = name;
            SeniorityLevel = seniority;
            TeamId = teamId;
            IsActive = isActive;
        }
        public Agent(Guid id, string username, string name, SeniorityLevel seniority, int teamId, bool isActive, DateTimeOffset datetimeCreated)
            : base(id, username, datetimeCreated)
        {
            Name = name;
            SeniorityLevel = seniority;
            TeamId = teamId;
            IsActive = isActive;
        }

        public int Capacity => (int)(TeamConstants.MaxConcurrencyCapacity * GetSeniorityMultiplier());

        private double GetSeniorityMultiplier() => SeniorityLevel switch
        {
            SeniorityLevel.Junior => 0.4,
            SeniorityLevel.Mid => 0.6,
            SeniorityLevel.Senior => 0.8,
            SeniorityLevel.TeamLead => 0.5,
            _ => 0
        };
    }
}
