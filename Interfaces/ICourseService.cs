using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface ICourseService
    {
        Task<PagedResult<Course>> GetAllAsync(int pageNumber, int pageSize);
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(CreateCourseDto dto);
        Task<Course?> UpdateAsync(int id, UpdateCourseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
