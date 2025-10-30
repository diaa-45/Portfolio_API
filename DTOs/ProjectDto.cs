namespace Portfolio_API.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? ImageCover { get; set; }
        // List of relative paths for images
        public List<ProjectImageDto> Images { get; set; } = new List<ProjectImageDto>();

        public string? DemoLink { get; set; }
    }
    public class ProjectImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
