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

        // delete image by id
        public async Task<bool> DeleteImageAsync(int projectId, int imageId)
        {
            var project = await _context.Projects
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null) return false;

            var image = project.Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null) return false;
            project.Images.Remove(image);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectImages> AddImageAsync(int projectId, string url)
        {
            var image = new ProjectImages
            {
                ProjectId = projectId,
                ImageUrl = url
            };

            _context.ProjectImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

    }
}
