namespace Portfolio_API.Services
{
    public interface IImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderName);
    }
}
