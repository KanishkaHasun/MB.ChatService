using ChatService.Domain.Enums;

namespace ChatService.Application.Models.Dtos
{
    public class AgentDto
    {
        public Guid AgentId { get; set; }
        public SeniorityLevel Seniority { get; set; }
        public Shift Shift { get; set; }
        public int AvailableSlots { get; set; } 
    }
}
