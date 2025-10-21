using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;
using System.Threading.Tasks;

namespace Portfolio_API.Repositories
{
    public class ArticleImageRepository : IArticleImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article?> GetByIdAsync(int articleId)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }
    }
}
