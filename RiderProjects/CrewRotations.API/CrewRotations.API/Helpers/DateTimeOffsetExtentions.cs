using System;

namespace CrewLibrary.API.Helpers
{
    public static class DateTimeOffsetExtentions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;
            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }

    }
}