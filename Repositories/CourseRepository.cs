using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _context.Courses.FindAsync(id);
            if (exist == null) return false;

            _context.Courses.Remove(exist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Course>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses.AsNoTracking().OrderByDescending(c => c.Id);

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Course>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> UpdateAsync(Course course)
        {
            // get course by id
             var existing = _context.Courses.FirstOrDefault(c => c.Id == course.Id);

            if (existing == null) return null;
            // Update all properties from the course parameter
            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.ImageCover = course.ImageCover;
            existing.InstructorName = course.InstructorName;
            existing.ContactLink = course.ContactLink;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
