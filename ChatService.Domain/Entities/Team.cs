using ChatService.Domain.Enums;

namespace ChatService.Domain.Entities
{
    public class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Shift Shift { get; private set; }
        public IList<Agent> Agents { get; private set; } = new List<Agent>();

        protected Team() { }

        public Team(int id, string name, Shift shift)
        {
            Id = id;
            Name = name;
            Shift = shift;
        }

        public double TotalCapacity => Agents.Sum(a => a.Capacity);

        public int MaxQueueSize => (int) Math.Round(TotalCapacity * 1.5);


    }
}
