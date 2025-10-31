using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.DTOs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        /// <summary>
        /// Get the About record
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<About>> GetAbout()
        {
            try
            {
                var about = await _aboutService.GetAbout();

                if (about == null)
                    return NotFound(new { message = "About record not found. Please create one first." });

                return Ok(about);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the about record.", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<About>> CreateAbout([FromForm] CreateAboutDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var about = await _aboutService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetAbout), new { id = about.Id }, about);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the about record.", error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<About>> UpdateAbout([FromForm] UpdateAboutDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var about = await _aboutService.UpdateAsync(dto);
                return Ok(about);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the about record.", error = ex.Message });
            }
        }
    }
}