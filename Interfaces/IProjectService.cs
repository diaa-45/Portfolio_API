using Portfolio_API.DTOs;

namespace Portfolio_API.Interfaces
{
    public interface IProjectService
    {
        Task<PagedResult<ProjectDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectDto> AddAsync(CreateProjectDto dto);
        Task<ProjectDto?> UpdateAsync(int id,UpdateProjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
