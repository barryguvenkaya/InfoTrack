namespace InfoTrack.Extensions;

using InfoTrack.Helpers;

public static class ExtensionMethods
{
    /// <summary>
    /// Accepts xx:xx format (24 hour)
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TimeOnly ToTimeOnly(this string time)
    {
        try
        {
            var split = time.Split(':');
            var hour = Convert.ToInt32(split[0]);
            var minute = Convert.ToInt32(split[1]);
            return new TimeOnly(hour, minute);
        }
        catch
        {
            throw new AppException($"ToTimeOnly failed for {time}.");
        }
    }
}