namespace ChatService.Application.Models.Dtos
{
    public class ChatStatusDto
    {
        public Guid UserId { get; set; }
        public Guid? AgentId { get; set; }
        public string ChatStatus { get; set; } = string.Empty;
    }
}
