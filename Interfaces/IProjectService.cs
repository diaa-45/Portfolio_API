using Portfolio_API.DTOs;

namespace Portfolio_API.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectDto> AddAsync(CreateProjectDto dto);
        Task<ProjectDto?> UpdateAsync(UpdateProjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
