
namespace Portfolio_API.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderName)
        {
            var savedPaths = new List<string>();

            if (files == null || files.Count == 0) return savedPaths;
            string uploadPath = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // relative path (for DB)
                    savedPaths.Add($"/{folderName}/{fileName}");
                }
            }

            return savedPaths;
        }
    }
}
