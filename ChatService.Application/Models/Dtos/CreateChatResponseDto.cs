namespace ChatService.Application.Models.Dtos
{
    public class CreateChatResponseDto
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public string ChatStatus { get; set; } = string.Empty;
    }
}
