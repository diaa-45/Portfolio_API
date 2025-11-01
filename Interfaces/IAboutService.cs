using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IAboutService
    {
        Task<About?> GetAbout();
        Task<About> CreateAsync(CreateAboutDto dto);
        Task<About?> UpdateAsync(UpdateAboutDto dto);
    }
}
