using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Interfaces;
using Portfolio_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IProjectImageRepository _projectImageRepo;
        private readonly IArticleImageRepository _articleImageRepo;

        public ImagesController(
            IImageService imageService,
            IProjectImageRepository projectImageRepo,
            IArticleImageRepository articleImageRepo)
        {
            _imageService = imageService;
            _projectImageRepo = projectImageRepo;
            _articleImageRepo = articleImageRepo;
        }

        // ========================= Projects =========================
        [HttpGet("projects/{projectId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjectImages(int projectId)
        {
            var images = await _projectImageRepo.GetAllByProjectIdAsync(projectId);
            return Ok(images);
        }

        [HttpPost("projects/{projectId}")]
        public async Task<IActionResult> UploadProjectImages(int projectId, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            var savedPaths = await _imageService.UploadImagesAsync(files, "projects");
            await _projectImageRepo.AddImagesAsync(projectId, savedPaths);

            return Ok(savedPaths);
        }

        [HttpDelete("projects/{projectId}/images/{imageId}")]
        public async Task<IActionResult> DeleteProjectImage(int projectId, int imageId)
        {
            // Get image record from repository
            var image = await _projectImageRepo.GetByIdAsync(imageId);
            if (image == null || image.ProjectId != projectId)
                return NotFound();

            // Delete the file using the relative path
            if (!string.IsNullOrEmpty(image.ImageUrl))
                await _imageService.DeleteImageAsync(image.ImageUrl);

            // Remove the image record from DB
            await _projectImageRepo.DeleteAsync(imageId);

            return NoContent();
        }



        // ========================= Articles =========================
        [HttpPost("articles/{articleId}/{type}")]
        public async Task<IActionResult> UploadArticleImage(int articleId, string type, [FromForm] IFormFile file)
        {
            if (file == null) return BadRequest("No image uploaded.");
            if (type != "main" && type != "thumb") return BadRequest("Invalid image type.");

            var savedPath = await _imageService.UploadSingleImageAsync(file, "articles");

            var article = await _articleImageRepo.GetByIdAsync(articleId);
            if (article == null) return NotFound();

            if (type == "main")
            {
                if (!string.IsNullOrEmpty(article.MainImage))
                    await _imageService.DeleteImageAsync(article.MainImage);

                article.MainImage = savedPath;
            }
            else
            {
                if (!string.IsNullOrEmpty(article.ThumbImage))
                    await _imageService.DeleteImageAsync(article.ThumbImage);

                article.ThumbImage = savedPath;
            }

            await _articleImageRepo.UpdateAsync(article);
            return Ok(new { path = savedPath });
        }

        [HttpDelete("articles/{articleId}/{type}")]
        public async Task<IActionResult> DeleteArticleImage(int articleId, string type)
        {
            if (type != "main" && type != "thumb") return BadRequest("Invalid image type.");

            var article = await _articleImageRepo.GetByIdAsync(articleId);
            if (article == null) return NotFound();

            string? imagePath = type == "main" ? article.MainImage : article.ThumbImage;

            if (!string.IsNullOrEmpty(imagePath))
                await _imageService.DeleteImageAsync(imagePath);

            if (type == "main") article.MainImage = null;
            else article.ThumbImage = null;

            await _articleImageRepo.UpdateAsync(article);

            return NoContent();
        }

    }
}
