using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IArticleRepository
    {
        Task<PagedResult<Article>> GetAllAsync(int pageNumber, int pageSize);
        Task<Article?> GetByIdAsync(int id);
        Task<Article> AddAsync(Article article);
        Task<Article?> UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
