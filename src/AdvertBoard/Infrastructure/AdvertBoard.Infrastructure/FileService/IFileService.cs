namespace AdvertBoard.Infrastructure.FileService
{
    public interface IFileService
    {
        string GetUniqueFileName(string fileName);
    }
}