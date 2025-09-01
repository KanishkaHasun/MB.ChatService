using ChatService.Domain.Entities;
using ChatService.Domain.Enums;
using System;

namespace ChatService.Infrastructure.Data.SeedData
{
    internal static class AgentData
    {
        internal static Agent[] GetAgents() =>
            new Agent[]
            {
                // Team A (TeamId = 1)
                new Agent(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    "lead01",
                    "Lead 01",
                    SeniorityLevel.TeamLead,
                    1,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    "mid01",
                    "Mid 01",
                    SeniorityLevel.Mid,
                    1,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                    "mid02",
                    "Mid 02",
                    SeniorityLevel.Mid,
                    1,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                    "junior01",
                    "Junior 01",
                    SeniorityLevel.Junior,
                    1,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),

                // Team B (TeamId = 2)
                new Agent(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                    "senior01",
                    "Senior 01",
                    SeniorityLevel.Senior,
                    2,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                    "mid03",
                    "Mid 03",
                    SeniorityLevel.Mid,
                    2,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                    "junior02",
                    "Junior 02",
                    SeniorityLevel.Junior,
                    2,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                    "junior03",
                    "Junior 03",
                    SeniorityLevel.Junior,
                    2,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),

                // Team C (TeamId = 3, Night shift)
                new Agent(
                    Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1"),
                    "mid04",
                    "Mid 04",
                    SeniorityLevel.Mid,
                    3,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc2"),
                    "mid05",
                    "Mid 05",
                    SeniorityLevel.Mid,
                    3,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),

                // Overflow Team (TeamId = 4, all considered Junior)
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd1"),
                    "junior04",
                    "Junior 04",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd2"),
                    "junior05",
                    "Junior 05",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd3"),
                    "junior06",
                    "Junior 06",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd4"),
                    "junior07",
                    "Junior 07",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd5"),
                    "junior08",
                    "Junior 08",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                ),
                new Agent(
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddd6"),
                    "junior09",
                    "Junior 09",
                    SeniorityLevel.Junior,
                    4,
                    true,
                    new DateTimeOffset(2025, 9, 1, 0, 0, 0, TimeSpan.Zero)
                )
            };
    }
}
