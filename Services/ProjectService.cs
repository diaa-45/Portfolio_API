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
                Images = new List<ProjectImage>()
            };

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
            var project = new Project
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DemoLink = dto.DemoLink,
                Images = new List<ProjectImage>()
            };

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
