namespace ChatService.Domain.Entities.Common
{
    public class BaseUser
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public DateTimeOffset DatetimeCreated { get; private set; }

        public BaseUser(string? username = null) : this(Guid.NewGuid(), username ?? Guid.NewGuid().ToString(), DateTimeOffset.UtcNow)
        {
        }

        public BaseUser(Guid id, string username , DateTimeOffset datetimeCreated)
        {
            Id = id;
            Username = username;
            DatetimeCreated = datetimeCreated;
        }
    }
}
