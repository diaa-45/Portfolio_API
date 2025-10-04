public class CreateProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DemoLink { get; set; }
    public IFormFile? ImageCover { get; set; }
    public List<IFormFile>? Images { get; set; }
}