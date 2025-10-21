using Portfolio_API.Models;

public class ProjectImages
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}
