namespace Storyline.WebApp.Common;

public class Helper
{
    public struct DateHelper
    {
        public static string TimeLineInfo(DateTime startDateTime, DateTime? endDateTime)
        {
            string result = startDateTime.ToString("MMMM yyyy");
            if (endDateTime.HasValue)
            {
                result += $" - {endDateTime.Value:MMMM yyyy}";
            }

            return result;

        }
    }
}
