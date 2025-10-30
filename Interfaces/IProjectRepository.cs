using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IProjectRepository
    {
        Task<PagedResult<Project>> GetAllAsync(int pageNumber , int pageSize);
        Task<Project?> GetByIdAsync(int id);
        Task<Project> AddAsync(Project project);
        Task<Project?> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}
