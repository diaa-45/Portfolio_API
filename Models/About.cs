namespace Portfolio_API.Models
{
    public class About
    {
        public int Id { get; set; }
        public string? OpeningImage { get; set; }
        public string? OpeningText { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Mission { get; set; }
        public string? Vision { get; set; }
        public string? Values { get; set; }
        public string? LinkedIn { get; set; }
        public string? GitHub { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
