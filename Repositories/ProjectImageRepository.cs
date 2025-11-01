using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_API.Repositories
{
    public class ProjectImageRepository : IProjectImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectImages>> GetAllByProjectIdAsync(int projectId)
        {
            return await _context.ProjectImages
                .Where(i => i.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task AddImagesAsync(int projectId, IEnumerable<string> urls)
        {
            var images = urls.Select(url => new ProjectImages
            {
                ProjectId = projectId,
                ImageUrl = url
            });

            _context.ProjectImages.AddRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectImages?> GetByIdAsync(int imageId)
        {
            return await _context.ProjectImages.FindAsync(imageId);
        }

        public async Task DeleteAsync(int imageId)
        {
            var img = await _context.ProjectImages.FindAsync(imageId);
            if (img != null)
            {
                _context.ProjectImages.Remove(img);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
