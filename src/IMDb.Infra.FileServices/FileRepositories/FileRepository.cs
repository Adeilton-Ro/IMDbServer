using IMDb.Infra.FileSystem.Abstraction;
using IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;

namespace IMDb.Infra.FileSystem.FileRepositories;
public class FileRepository : IFileRepository
{
    public string SaveDirectorImages(FileImage image, string ImageName)
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
}
