namespace Portfolio_API.Services
{
    public interface IImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderName);
        Task<string> UploadSingleImageAsync(IFormFile file, string folderName);
        Task DeleteImageAsync(string imageRelativePath);
    }
}
