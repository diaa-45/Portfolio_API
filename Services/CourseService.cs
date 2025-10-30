using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseleRepo;
        private readonly IImageService _imageService;

        public CourseService(ICourseRepository courseleRepo, IImageService imageService)
        {
            _courseleRepo = courseleRepo;
            _imageService = imageService;
        }
        public async Task<PagedResult<Course>> GetAllAsync(int pageNumber, int pageSize) =>
            await _courseleRepo.GetAllAsync(pageNumber, pageSize);

        public async Task<Course?> GetByIdAsync(int id) =>
            await _courseleRepo.GetByIdAsync(id);

        public async Task<Course> CreateAsync(CreateCourseDto dto)
        {
            string? ImagePath = null;

            if (dto.ImageCover != null)
                ImagePath = await _imageService.UploadSingleImageAsync(dto.ImageCover, "courses");

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                ContactLink = dto.ContactLink,
                ImageCover = ImagePath,
                InstructorName = dto.InstructorName
            };

            await _courseleRepo.AddAsync(course);
            return course;
        }

        public async Task<Course?> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var existing = await _courseleRepo.GetByIdAsync(id);
            if (existing == null) return null;

            // Only update if the value is provided (not null)
            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Description != null) existing.Description = dto.Description;
            if (dto.InstructorName != null) existing.InstructorName = dto.InstructorName;
            if (dto.ContactLink != null) existing.ContactLink = dto.ContactLink;

            if (dto.ImageCover != null)
            {
                if (!string.IsNullOrEmpty(existing.ImageCover))
                    await _imageService.DeleteImageAsync(existing.ImageCover);
                existing.ImageCover = await _imageService.UploadSingleImageAsync(dto.ImageCover, "courses");
            }

            return await _courseleRepo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _courseleRepo.GetByIdAsync(id);
            if (article == null) return false;

            if (!string.IsNullOrEmpty(article.ImageCover))
                await _imageService.DeleteImageAsync(article.ImageCover);

            await _courseleRepo.DeleteAsync(id);
            return true;
        }

    }
}
