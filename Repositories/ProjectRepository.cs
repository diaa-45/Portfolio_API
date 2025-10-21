using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                    .Select(p => new Project
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        ImageCover = p.ImageCover,
                        DemoLink = p.DemoLink,
                        Images = p.Images
                            .Select(pi => new ProjectImages { ImageUrl = pi.ImageUrl })
                            .ToList()
                    })
                    .ToListAsync();
                    }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.Where(p => p.Id == id)
            .Select(p => new Project
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageCover = p.ImageCover,
                DemoLink = p.DemoLink,
                Images = p.Images
                    .Select(pi => new ProjectImages { ImageUrl = pi.ImageUrl })
                    .ToList()
            })
            .FirstOrDefaultAsync(); 
        }
        public async Task<Project> AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<Project?> UpdateAsync(Project project)
        {
            var existing = await _context.Projects
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == project.Id);

            if (existing == null) return null;

            existing.Title = project.Title?? existing.Title;
            existing.Description = project.Description?? existing.Description;
            existing.DemoLink = project.DemoLink ?? existing.DemoLink;
            existing.ImageCover = project.ImageCover ?? existing.ImageCover;

            // Add new images if any
            if (project.Images != null && project.Images.Any())
            {
                foreach (var img in project.Images)
                {
                    existing.Images.Add(img);
                }
            }

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
