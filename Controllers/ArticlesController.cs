using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;
using Portfolio_API.Services;

namespace Portfolio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepo;

        public ArticlesController(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _articleRepo.GetAllAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var article = await _articleRepo.GetByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            await _articleRepo.AddAsync(article);
            await _articleRepo.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Article article)
        {
            var existing = await _articleRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = article.Title;
            existing.Content = article.Content;
            existing.MainImage = article.MainImage;
            existing.ThumbImage = article.ThumbImage;

            await _articleRepo.UpdateAsync(existing);
            await _articleRepo.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleRepo.DeleteAsync(id);
            await _articleRepo.SaveAsync();
            return NoContent();
        }
    }
}
