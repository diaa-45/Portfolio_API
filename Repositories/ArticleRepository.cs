using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles
                .OrderByDescending(a => a.Date)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
        }
        public async Task DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
                _context.Articles.Remove(article);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
