using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Interfaces
{
    public interface IContactFormService
    {
        Task<ContactForm> CreateAsync(ContactFormDto dto);
         IEnumerable<ContactForm> GetAll();
        // mark message as read
        Task MarkAsReadAsync(int id);
    }
}
