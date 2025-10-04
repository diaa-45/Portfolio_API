using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;
        private readonly IImageService _imageService;

        public ProjectService(IProjectRepository repo, IImageService imageService)
        {
            _repo = repo;
            _imageService = imageService;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _repo.GetAllAsync();
            return projects.Select(MapToDto);
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _repo.GetByIdAsync(id);
            if (project == null) return null;
            return MapToDto(project);
        }
        public async Task<ProjectDto> AddAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                DemoLink = dto.DemoLink,
                ImageCover = "",
                Images = new List<ProjectImage>()
            };
            if(dto.ImageCover.Length > 0 && dto.ImageCover != null)
            {
                var path = await _imageService.UploadImageAsync(dto.ImageCover, "projects");
                project.ImageCover = path;
            }
            if (dto.Images != null && dto.Images.Any())
            {
                var paths = await _imageService.UploadImagesAsync(dto.Images, "projects");
                project.Images = paths.Select(p => new ProjectImage { ImageUrl = p }).ToList();
            }

            await _repo.AddAsync(project);
            return MapToDto(project);
        }
        public async Task<ProjectDto?> UpdateAsync(UpdateProjectDto dto)
        {
            // 1. Load project from DB with existing images
            var project = await _repo.GetByIdAsync(dto.Id);
            if (project == null)
                return null;
            // 2. Update scalar properties
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.DemoLink = dto.DemoLink;
            project.ImageCover = project.ImageCover; // keep existing cover if no new provided

            if (dto.NewImageCover != null && dto.NewImageCover.Length > 0)
            {
                var path = await _imageService.UploadImageAsync(dto.NewImageCover, "projects");
                project.ImageCover = path;
            }
            if (dto.NewImages != null && dto.NewImages.Any())
            {
                var paths = await _imageService.UploadImagesAsync(dto.NewImages, "projects");
                project.Images = paths.Select(p => new ProjectImage { ImageUrl = p }).ToList();
            }

            var updated = await _repo.UpdateAsync(project);
            return updated == null ? null : MapToDto(updated);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        // Map Project to ProjectDto
        private ProjectDto MapToDto(Project p)
        {
            return new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                DemoLink = p.DemoLink,
                Images = p.Images.Select(i => i.ImageUrl).ToList()
            };
        }
    }
}
