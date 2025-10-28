namespace Portfolio_API.DTOs
{
    public class UpdateArticleDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? Content { get; set; }
        public string? Author { get; set; } 
        public DateTime Date { get; set; }= DateTime.Now;

        public IFormFile? MainImage { get; set; }
        public IFormFile? ThumbImage { get; set; }

        // internal storage of paths
        public string? MainImagePath { get; set; }
        public string? ThumbImagePath { get; set; }
    }
}
