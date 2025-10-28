using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> AddAsync(Article article);
        Task<Article?> UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
