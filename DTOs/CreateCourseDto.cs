namespace Portfolio_API.DTOs
{
    public class CreateCourseDto
    {
        public string Title { get; set; }=null!;
        public string? Description { get; set; }
        public IFormFile? ImageCover { get; set; }
        public string? InstructorName { get; set; }
        public string? ContactLink { get; set; }
    }
}
