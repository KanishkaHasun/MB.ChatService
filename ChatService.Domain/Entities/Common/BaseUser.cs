namespace ChatService.Domain.Entities.Common
{
    public class BaseUser
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public DateTimeOffset DatetimeCreated { get; private set; }

        public BaseUser(string? username = null)
        {
            Id = Guid.NewGuid();
            Username = username ?? string.Empty;
            DatetimeCreated = DateTime.UtcNow;
        }
    }
}
