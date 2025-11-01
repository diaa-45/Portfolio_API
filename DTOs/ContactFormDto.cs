using System.ComponentModel.DataAnnotations;

namespace Portfolio_API.DTOs
{
    public class ContactFormDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
