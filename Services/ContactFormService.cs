using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Hubs;
using Portfolio_API.Interfaces;
using Portfolio_API.Models;

namespace Portfolio_API.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ContactFormService(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<ContactForm> CreateAsync(ContactFormDto dto)
        {
            var contact = new ContactForm
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message
            };

            _context.ContactForms.Add(contact);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                contact.Id,
                contact.Name,
                contact.Email,
                contact.Message,
                contact.CreatedAt
            });

            return contact;
        }

        public IEnumerable<ContactForm> GetAll() 
        {
            return  _context.ContactForms.OrderByDescending(c => c.CreatedAt);
        }

        // mark as read
        public async Task MarkAsReadAsync(int id)
        {
            var contact =await _context.ContactForms.FirstOrDefaultAsync(c => c.Id == id);
            if (contact != null)
            {
                contact.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

    }
}
