using IMDb.Infra.FileSystem.Abstraction;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;

namespace IMDb.Infra.FileSystem.FileRepositories;
public class FileRepository : IFileRepository
{
    public string SaveActorImage(FileImage image, string ImageName)
    {
        var path = $"Actor/{ImageName}{image.Extention}";

        var files = Directory.GetFiles("wwwroot/Actor/");
        var savePath = $"wwwroot/{path}";
        var existingFile = files.FirstOrDefault(f => f.Split(".")[0] == savePath.Split(".")[0]);

        if (existingFile is not null)
            File.Delete(existingFile);

        File.WriteAllBytes(savePath, image.Image);

        return path;
    }

    public string SaveDirectorImage(FileImage image, string ImageName)
    {
        var path = $"Director/{ImageName}{image.Extention}";

        var files = Directory.GetFiles("wwwroot/Director/");
        var savePath = $"wwwroot/{path}";
        var existingFile = files.FirstOrDefault(f => f.Split(".")[0] == savePath.Split(".")[0]);

        if (existingFile is not null)
            File.Delete(existingFile);

        File.WriteAllBytes(savePath, image.Image);

        return path;
    }

    public IEnumerable<string> SaveFilmImage(IEnumerable<NamedFileImage> images, string filmName)
    {
        Directory.CreateDirectory($"wwwroot/Film/{filmName}");

        var paths = images.Select(i => $"Film/{filmName}/{i.Name}{i.Image.Extention}");

        foreach (var image in images)
            File.WriteAllBytes($"wwwroot/Film/{filmName}/{image.Name}{image.Image.Extention}", image.Image.Image);

        return paths;
    }
}
