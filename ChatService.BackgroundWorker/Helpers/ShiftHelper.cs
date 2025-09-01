using ChatService.Domain.Enums;

namespace ChatService.BackgroundWorker.Helpers
{
    internal static class ShiftHelper
    {
        internal static Shift GetMainShift(DateTime utcNow)
        {
            var hour = utcNow.Hour;

            return hour switch
            {
                >= 6 and < 14 => Shift.Morning,
                >= 14 and < 22 => Shift.Evening,
                _ => Shift.Night
            };
        }

        internal static bool IsOverflowTeamActive(DateTime utcNow)
        {
            var hour = utcNow.Hour;
            return (hour >= 8 && hour < 17);

        }
    }
}
