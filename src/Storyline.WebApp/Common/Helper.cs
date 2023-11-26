namespace Storyline.WebApp.Common;

public class Helper
{
    public struct DateHelper
    {
        public static string TimeLineInfo(DateTime startDateTime, DateTime? endDateTime, bool isShort = false, bool showYearOnly = false)
        {
            string format = isShort ? "MMM yyyy" : "MMMM yyyy";
            format = showYearOnly ? "yyyy" : format;

            string result = startDateTime.ToString(format);
            if (endDateTime.HasValue)
            {
                result += $" - {endDateTime.Value.ToString(format)}";
            }

            return result;

        }
    }
}
