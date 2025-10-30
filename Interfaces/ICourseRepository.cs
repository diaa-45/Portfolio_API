using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface ICourseRepository
    {
        Task<PagedResult<Course>> GetAllAsync(int pageNumber, int pageSize);
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> AddAsync(Course course);
        Task<Course?> UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
    }
}
