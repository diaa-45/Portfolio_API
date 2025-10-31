using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Repositories
{
    public class AboutRepository : IAboutRepository
    {
        private readonly ApplicationDbContext _context;

        public AboutRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<About> GetAbout()
        {
            return await _context.About.FirstOrDefaultAsync();
        }

        public async Task<About> AddAsync(About about)
        {
            _context.About.Add(about);
            await _context.SaveChangesAsync();
            return about;
        }

        public async Task<About> UpdateAsync(About about)
        {
            _context.About.Update(about);
            await _context.SaveChangesAsync();
            return about;
        }

        
    }
}
