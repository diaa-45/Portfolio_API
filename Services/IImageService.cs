namespace Portfolio_API.Services
{
    public interface IImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderName);
        Task<string> UploadImageAsync(IFormFile file, string folderName);
    }
}
