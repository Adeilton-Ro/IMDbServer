namespace IMDb.Infra.FileSystem.Abstraction.Interfaces.FileRepositories;
public interface IFileRepository
{
    string SaveDirectorImage(FileImage image, string ImageName);
    string SaveActorImage(FileImage image, string ImageName);
}