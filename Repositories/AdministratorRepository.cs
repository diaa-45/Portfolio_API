using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly ApplicationDbContext _context;
        public AdministratorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Administrator?> GetByEmailAsync(string email)
        {
            return await _context.Administrators.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
