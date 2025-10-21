using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IArticleImageRepository
    {
        Task<Article?> GetByIdAsync(int articleId);
        Task UpdateAsync(Article article);
    }
}
