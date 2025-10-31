namespace Portfolio_API.DTOs
{
    public class CreateAboutDto
    {
        public IFormFile? OpeningImage { get; set; }
        public string? OpeningText { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Mission { get; set; }
        public string? Vision { get; set; }
        public string? Values { get; set; }
    }
}
