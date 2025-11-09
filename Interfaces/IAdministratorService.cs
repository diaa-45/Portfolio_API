using Portfolio_API.DTOs;

namespace Portfolio_API.Interfaces
{
    public interface IAdministratorService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<bool> ChangePasswordAsync(string email, ChangePasswordDTO request);
    }
}
