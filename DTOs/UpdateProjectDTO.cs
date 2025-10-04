public class UpdateProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DemoLink { get; set; }
    // File if uploading a new cover
    public IFormFile? NewImageCover { get; set; }
    public string? ImageCover { get; set; }
    public List<IFormFile>? NewImages { get; set; } // optional new uploads
}