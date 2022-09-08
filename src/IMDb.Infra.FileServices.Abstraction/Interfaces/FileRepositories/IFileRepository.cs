namespace IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
public interface IFileRepository
{
    string SaveDirectorImages(FileImage image, string NameImage);
}