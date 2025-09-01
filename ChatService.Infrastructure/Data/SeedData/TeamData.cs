using ChatService.Domain.Entities;
using ChatService.Domain.Enums;

namespace ChatService.Infrastructure.Data.SeedData
{
    internal static class TeamData
    {
        internal static Team[] GetTeams() =>
            new Team[]
            {
                new Team(1, "Team A", Shift.Morning),
                new Team(2, "Team B", Shift.Evening),
                new Team(3, "Team C", Shift.Night),
                new Team(4, "Overflow Team", Shift.OfficeHours)
            };
    }
}
