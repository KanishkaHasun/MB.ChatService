namespace ChatService.Domain.Entities
{
    public class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public IList<Agent> Agents { get; private set; } = new List<Agent>();

    }
}
