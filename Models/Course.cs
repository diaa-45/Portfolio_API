namespace Portfolio_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageCover { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string ContactLink { get; set; } = string.Empty;
    }
}
