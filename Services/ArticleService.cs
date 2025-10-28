using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IImageService _imageService;

        public ArticleService(IArticleRepository articleRepo, IImageService imageService)
        {
            _articleRepo = articleRepo;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Article>> GetAllAsync() =>
            await _articleRepo.GetAllAsync();

        public async Task<Article?> GetByIdAsync(int id) =>
            await _articleRepo.GetByIdAsync(id);

        public async Task<Article> CreateAsync(CreateArticleDto dto)
        {
            string? mainImagePath = null;
            string? thumbImagePath = null;

            if (dto.MainImage != null)
                mainImagePath = await _imageService.UploadSingleImageAsync(dto.MainImage, "articles");

            if (dto.ThumbImage != null)
                thumbImagePath = await _imageService.UploadSingleImageAsync(dto.ThumbImage, "articles");

            var article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                Date = dto.Date,
                MainImage = mainImagePath,
                ThumbImage = thumbImagePath
            };

            await _articleRepo.AddAsync(article);
            return article;
        }

        public async Task<Article?> UpdateAsync(int id, UpdateArticleDto dto)
        {
            var existing = await _articleRepo.GetByIdAsync(id);
            if (existing == null) return null;

            // Only update if the value is provided (not null)
            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Content != null) existing.Content = dto.Content;
            if (dto.Author != null) existing.Author = dto.Author;
            existing.Date = DateTime.Now; 

            if (dto.MainImage != null)
            {
                if (!string.IsNullOrEmpty(existing.MainImage))
                    await _imageService.DeleteImageAsync(existing.MainImage);
                existing.MainImage = await _imageService.UploadSingleImageAsync(dto.MainImage, "articles");
            }

            if (dto.ThumbImage != null)
            {
                if (!string.IsNullOrEmpty(existing.ThumbImage))
                    await _imageService.DeleteImageAsync(existing.ThumbImage);
                existing.ThumbImage = await _imageService.UploadSingleImageAsync(dto.ThumbImage, "articles");
            }

            return await _articleRepo.UpdateAsync(existing);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _articleRepo.GetByIdAsync(id);
            if (article == null) return false;

            if (!string.IsNullOrEmpty(article.MainImage))
                await _imageService.DeleteImageAsync(article.MainImage);

            if (!string.IsNullOrEmpty(article.ThumbImage))
                await _imageService.DeleteImageAsync(article.ThumbImage);

            await _articleRepo.DeleteAsync(id);
            return true;
        }


    }
}
