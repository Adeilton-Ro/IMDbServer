namespace IMDb.Application;
public class Utils
{
    public static Guid? TryParseNullSafe(string? id)
    {
        if (id is null)
            return default;

        if (Guid.TryParse(id, out Guid result))
            return result;

        return default;
    }
}