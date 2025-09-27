using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<Administrator?> GetByEmailAsync(string email);
    }
}
