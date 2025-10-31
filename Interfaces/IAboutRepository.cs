using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IAboutRepository
    {
        Task<About> GetAbout();
        Task<About> AddAsync(About about);
        Task<About> UpdateAsync(About about);
    }
}
