public class UpdateProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DemoLink { get; set; }
    public List<IFormFile>? NewImages { get; set; } // optional new uploads
}