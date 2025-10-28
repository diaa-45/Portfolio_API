namespace Portfolio_API.DTOs
{
    public class CreateArticleDto
    {
        public string? Title { get; set; } 
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Uploaded images
        public IFormFile? MainImage { get; set; }
        public IFormFile? ThumbImage { get; set; }
    }
}
