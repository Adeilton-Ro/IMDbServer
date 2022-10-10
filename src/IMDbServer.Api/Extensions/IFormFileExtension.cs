namespace IMDbServer.Api.Extensions;
public static class IFormFileExtension
{
    public static byte[] ToBytes(this IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }
}