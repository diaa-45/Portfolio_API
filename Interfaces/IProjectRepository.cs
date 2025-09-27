using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> AddAsync(Project project);
        Task<Project?> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}
