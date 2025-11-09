using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<bool> ChangePasswordAsync(string email, ChangePasswordDTO request)
        {
            var admin = await _repo.GetByEmailAsync(email);
            if (admin == null)
                return false;

            var verifyResult = _passwordHasher.VerifyHashedPassword(email, admin.PasswordHash, request.CurrentPassword);
            if (verifyResult == PasswordVerificationResult.Failed)
                return false;

            admin.PasswordHash = _passwordHasher.HashPassword(email, request.NewPassword);
            await _repo.UpdateAsync(admin);
            return true;
        }
    }
}
