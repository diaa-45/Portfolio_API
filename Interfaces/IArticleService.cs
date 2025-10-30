using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IArticleService
    {
        Task<PagedResult<Article>> GetAllAsync(int pageNumber, int pageSize);
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(CreateArticleDto dto);
        Task<Article?> UpdateAsync(int id,UpdateArticleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
