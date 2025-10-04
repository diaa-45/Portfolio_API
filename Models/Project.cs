namespace Portfolio_API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageCover { get; set; }
        // One-to-many relationship
        public string? DemoLink { get; set; }
        public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
    }
}
