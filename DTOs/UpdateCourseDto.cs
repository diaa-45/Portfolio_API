namespace Portfolio_API.DTOs
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public IFormFile? ImageCover { get; set; }
        public string? InstructorName { get; set; }
        public string? ContactLink { get; set; }
    }
}
