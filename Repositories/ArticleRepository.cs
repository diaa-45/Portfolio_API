// Repositories/ArticleRepository.cs
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .AsNoTracking()
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article> AddAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article?> UpdateAsync(Article article)
        {
            var existing = await _context.Articles
                .FirstOrDefaultAsync(p => p.Id == article.Id); // NO AsNoTracking

            if (existing == null) return null;

            // Update all properties from the article parameter
            existing.Title = article.Title;
            existing.Content = article.Content;
            existing.Date = article.Date;
            existing.AuthorName = article.AuthorName;
            existing.MainImage = article.MainImage;
            existing.ThumbImage = article.ThumbImage;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _context.Articles.FindAsync(id);
            if (exist == null) return false;

            _context.Articles.Remove(exist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
