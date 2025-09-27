using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;

namespace Portfolio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdministratorService _service;

        public AuthController(IAdministratorService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            if (response == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(response);
        }
    }
}
