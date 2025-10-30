using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
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

        public async Task<PagedResult<Project>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Projects
                    .AsNoTracking()
                    .Include(p => p.Images) // ✅ Eager load to avoid N+1
                    .OrderByDescending(p => p.Id); 

            var totalCount = await query.CountAsync();
            // Get paginated data with projection
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new Project
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImageCover = p.ImageCover,
                    DemoLink = p.DemoLink,
                    Images = p.Images
                        .Select(img => new ProjectImages // ✅ Use different variable name
                        {
                            Id = img.Id,
                            ImageUrl = img.ImageUrl
                        }).ToList()
                }).ToListAsync();
            return new PagedResult<Project>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
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
                Images = p.Images.Select(p => new ProjectImages { Id = p.Id, ImageUrl = p.ImageUrl }).ToList()

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
