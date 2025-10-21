namespace Portfolio_API.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string? MainImage { get; set; }   // صورة رئيسية
        public string? ThumbImage { get; set; }  // صورة مصغرة
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
