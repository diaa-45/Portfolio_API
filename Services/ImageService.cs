
using Portfolio_API.Interfaces;

namespace Portfolio_API.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IProjectImageRepository _repository;

        public ImageService(IWebHostEnvironment env, IProjectImageRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderName)
        {
            var savedPaths = new List<string>();
            var uploadPath = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);
                savedPaths.Add($"/{folderName}/{fileName}");
            }

            return savedPaths;
        }
        
        public async Task<string> UploadSingleImageAsync(IFormFile file, string folderName)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{folderName}/{fileName}";
        }

        public async Task DeleteImageAsync(string imageRelativePath)
        {
            if (string.IsNullOrWhiteSpace(imageRelativePath))
                return;

            var sanitizedPath = imageRelativePath.TrimStart('/').Replace("..", "");
            var fullPath = Path.Combine(_env.WebRootPath, sanitizedPath);

            try
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
            catch
            {
                // Optional: log the exception
            }

            await Task.CompletedTask; // keep async signature
        }
    }
}
