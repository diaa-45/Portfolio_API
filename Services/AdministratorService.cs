using Microsoft.AspNetCore.Identity;
using Portfolio_API.DTOs;
using Portfolio_API.Helpers;
using Portfolio_API.Interfaces;

namespace Portfolio_API.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _repo;
        private readonly JwtHelper _jwt;
        private readonly PasswordHasher<string> _passwordHasher;

        public AdministratorService(IAdministratorRepository repo, JwtHelper jwt)
        {
            _repo = repo;
            _jwt = jwt;
            _passwordHasher = new PasswordHasher<string>();
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var admin = await _repo.GetByEmailAsync(request.Email);
            if (admin == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(request.Email, admin.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return new LoginResponse
            {
                Token = _jwt.GenerateToken(admin),
                Name = admin.Name,
                Email = admin.Email
            };
        }
    }
}
